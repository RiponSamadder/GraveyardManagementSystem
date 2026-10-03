namespace GMS.Application.DTOs;

public record LoginRequestDto(string UserName, string Password);

public record LoginResponseDto(
    long UserId,
    string UserCode,
    string FullName,
    string? Email,
    string? MobileNo,
    string Token,
    DateTime ExpiresAt,
    List<string> Roles
);

public record RegisterUserDto(
    string UserCode,
    string FullName,
    string? FullNameBn,
    string UserName,
    string Password,
    string? Email,
    string? MobileNo,
    string? Address
);

public record ChangePasswordDto(string OldPassword, string NewPassword);

public record UserProfileDto(
    long UserId,
    string UserCode,
    string FullName,
    string? FullNameBn,
    string? Email,
    string? MobileNo,
    string? Address,
    bool IsActive,
    DateTime CreatedAt,
    List<string> Roles
);
