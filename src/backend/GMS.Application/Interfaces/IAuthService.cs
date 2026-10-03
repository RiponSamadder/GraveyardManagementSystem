using GMS.Application.DTOs;

namespace GMS.Application.Interfaces;

public interface IAuthService
{
    Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    Task<ApiResponse<UserProfileDto>> RegisterAsync(RegisterUserDto request);
    Task<ApiResponse<UserProfileDto>> GetProfileAsync(long userId);
    Task<ApiResponse<bool>> ChangePasswordAsync(long userId, ChangePasswordDto request);
    Task<ApiResponse<List<UserProfileDto>>> GetAllUsersAsync();
}
