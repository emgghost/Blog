using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> Register(UserRegisterDto registerDto)
    {
        var user = await _userService.RegisterAsync(registerDto);
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserResponseDto>> Login(UserLoginDto loginDto)
    {
        var user = await _userService.LoginAsync(loginDto);
        return Ok(user);
    }

    [HttpGet("current")]
    [Authorize]
    public async Task<ActionResult<UserResponseDto>> GetCurrentUser()
    {
        var user = await _userService.GetCurrentUserAsync(User);
        return Ok(user);
    }
}