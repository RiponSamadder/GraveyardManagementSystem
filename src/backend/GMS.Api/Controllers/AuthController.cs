using System.Security.Claims;
using GMS.Application.DTOs;
using GMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMS.Api.Controllers;

[ApiController]
[Route("api/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;

    /// <summary>Login with username/email and password</summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var result = await _authService.LoginAsync(request);
        return result.Success ? Ok(result) : Unauthorized(result);
    }

    /// <summary>Register a new user account</summary>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
    {
        var result = await _authService.RegisterAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get current logged-in user profile</summary>
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _authService.GetProfileAsync(userId);
        return result.Success ? Ok(result) : NotFound(result);
    }

    /// <summary>Change password for current user</summary>
    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
    {
        var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _authService.ChangePasswordAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get all users (Admin only)</summary>
    [HttpGet("users")]
    [Authorize]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _authService.GetAllUsersAsync();
        return Ok(result);
    }
}
