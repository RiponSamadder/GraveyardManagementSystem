using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GMS.Application.DTOs;
using GMS.Application.Interfaces;
using GMS.Core.Entities;
using GMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GMS.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly GraveyardDbContext _db;
    private readonly IConfiguration _config;

    public AuthService(GraveyardDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var user = await _db.GmsAppUsers
            .Where(u => (u.UserName == request.UserName || u.Email == request.UserName) && u.IsActive && !u.IsBlocked)
            .FirstOrDefaultAsync();

        if (user == null)
            return ApiResponse<LoginResponseDto>.Fail("Invalid username or password.");

        if (!VerifyPassword(request.Password, user.PasswordHash))
            return ApiResponse<LoginResponseDto>.Fail("Invalid username or password.");

        var roles = await _db.GmsUserRoles
            .Where(ur => ur.UserId == user.UserId)
            .Join(_db.GmsRoles, ur => ur.RoleId, r => r.RoleId, (ur, r) => r.RoleName)
            .ToListAsync();

        user.LastLoginAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        var (token, expires) = GenerateToken(user, roles);

        return ApiResponse<LoginResponseDto>.Ok(new LoginResponseDto(
            user.UserId, user.UserCode, user.FullName,
            user.Email, user.MobileNo, token, expires, roles));
    }

    public async Task<ApiResponse<UserProfileDto>> RegisterAsync(RegisterUserDto request)
    {
        if (await _db.GmsAppUsers.AnyAsync(u => u.UserName == request.UserName))
            return ApiResponse<UserProfileDto>.Fail("Username already exists.");

        if (!string.IsNullOrEmpty(request.Email) &&
            await _db.GmsAppUsers.AnyAsync(u => u.Email == request.Email))
            return ApiResponse<UserProfileDto>.Fail("Email already registered.");

        var user = new GmsAppUser
        {
            UserCode = request.UserCode,
            FullName = request.FullName,
            FullNameBn = request.FullNameBn,
            UserName = request.UserName,
            PasswordHash = HashPassword(request.Password),
            Email = request.Email,
            MobileNo = request.MobileNo,
            Address = request.Address,
            IsActive = true,
            IsBlocked = false,
            IsEmailVerified = false,
            IsMobileVerified = false,
            CreatedAt = DateTime.UtcNow
        };

        _db.GmsAppUsers.Add(user);
        await _db.SaveChangesAsync();

        return ApiResponse<UserProfileDto>.Ok(MapToProfile(user, []), "User registered successfully.");
    }

    public async Task<ApiResponse<UserProfileDto>> GetProfileAsync(long userId)
    {
        var user = await _db.GmsAppUsers.FindAsync(userId);
        if (user == null) return ApiResponse<UserProfileDto>.Fail("User not found.");

        var roles = await _db.GmsUserRoles
            .Where(ur => ur.UserId == userId)
            .Join(_db.GmsRoles, ur => ur.RoleId, r => r.RoleId, (ur, r) => r.RoleName)
            .ToListAsync();

        return ApiResponse<UserProfileDto>.Ok(MapToProfile(user, roles));
    }

    public async Task<ApiResponse<bool>> ChangePasswordAsync(long userId, ChangePasswordDto request)
    {
        var user = await _db.GmsAppUsers.FindAsync(userId);
        if (user == null) return ApiResponse<bool>.Fail("User not found.");

        if (!VerifyPassword(request.OldPassword, user.PasswordHash))
            return ApiResponse<bool>.Fail("Current password is incorrect.");

        user.PasswordHash = HashPassword(request.NewPassword);
        user.ModifiedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "Password changed successfully.");
    }

    public async Task<ApiResponse<List<UserProfileDto>>> GetAllUsersAsync()
    {
        var users = await _db.GmsAppUsers
            .Where(u => u.IsActive)
            .OrderBy(u => u.FullName)
            .ToListAsync();

        var result = new List<UserProfileDto>();
        foreach (var u in users)
        {
            var roles = await _db.GmsUserRoles
                .Where(ur => ur.UserId == u.UserId)
                .Join(_db.GmsRoles, ur => ur.RoleId, r => r.RoleId, (ur, r) => r.RoleName)
                .ToListAsync();
            result.Add(MapToProfile(u, roles));
        }

        return ApiResponse<List<UserProfileDto>>.Ok(result);
    }

    // ── helpers ──────────────────────────────────────────────────────────────

    private (string token, DateTime expires) GenerateToken(GmsAppUser user, List<string> roles)
    {
        var secret = _config["JwtSettings:Secret"] ?? "DefaultSecretKey12345678901234567890!";
        var expiryMinutes = int.Parse(_config["JwtSettings:ExpiryMinutes"] ?? "1440");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(expiryMinutes);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Name, user.UserName ?? user.Email ?? user.UserCode),
            new("fullName", user.FullName),
            new("userCode", user.UserCode)
        };
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var token = new JwtSecurityToken(
            expires: expires,
            signingCredentials: creds,
            claims: claims);

        return (new JwtSecurityTokenHandler().WriteToken(token), expires);
    }

    private static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password), salt, 100_000, HashAlgorithmName.SHA256, 32);
        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    private static bool VerifyPassword(string password, string? storedHash)
    {
        if (string.IsNullOrEmpty(storedHash)) return false;
        var parts = storedHash.Split('.');
        if (parts.Length != 2) return false;
        var salt = Convert.FromBase64String(parts[0]);
        var expected = Convert.FromBase64String(parts[1]);
        var actual = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password), salt, 100_000, HashAlgorithmName.SHA256, 32);
        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }

    private static UserProfileDto MapToProfile(GmsAppUser u, List<string> roles) =>
        new(u.UserId, u.UserCode, u.FullName, u.FullNameBn,
            u.Email, u.MobileNo, u.Address, u.IsActive, u.CreatedAt, roles);
}
