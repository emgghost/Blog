using System.Security.Claims;
using Blog.Application.DTOs;

namespace Blog.Application.Interfaces;

public interface IUserService
{
    Task<UserResponseDto> RegisterAsync(UserRegisterDto registerDto);
    Task<UserResponseDto> LoginAsync(UserLoginDto loginDto);
    Task<UserResponseDto> GetCurrentUserAsync(ClaimsPrincipal user);
    // سایر متدها: تغییر رمز، بازیابی رمز و...
}