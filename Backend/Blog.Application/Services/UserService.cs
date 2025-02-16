using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Application.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<UserService> _logger;
    public UserService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IMapper mapper,
        IOptions<JwtSettings> jwtSettings,
        ILogger<UserService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _jwtSettings = jwtSettings.Value;
        _logger = logger;
    }

    public async Task<UserResponseDto> RegisterAsync(UserRegisterDto registerDto)
    {
        var user = _mapper.Map<ApplicationUser>(registerDto);
        user.UserName = registerDto.Email; // یا یک نام کاربری جداگانه
        
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                _logger.LogError($"Registration error: {error.Code} - {error.Description}");
            }
            throw new ApplicationException($"خطا در ثبت‌نام: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        
        // اختصاص نقش پیش‌فرض
        await _userManager.AddToRoleAsync(user, "User");
        
        return GenerateUserResponse(user);
    }

    public async Task<UserResponseDto> LoginAsync(UserLoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
            throw new ApplicationException("کاربر یافت نشد.");
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
            throw new ApplicationException("رمز عبور نادرست.");
        
        return GenerateUserResponse(user);
    }

    private UserResponseDto GenerateUserResponse(ApplicationUser user)
    {
        var token = GenerateJwtToken(user);
        return new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            DisplayName = user.DisplayName,
            Token = token
        };
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.DisplayName),
            // نقش‌ها
            new Claim(ClaimTypes.Role, "User") // یا دریافت از دیتابیس
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public async Task<UserResponseDto> GetCurrentUserAsync(ClaimsPrincipal user)
    {
        // استخراج Claim مربوط به شناسه کاربر
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new ApplicationException("کاربر احراز هویت نشده است.");

        // یافتن کاربر در دیتابیس
        var currentUser = await _userManager.FindByIdAsync(userId);
        if (currentUser == null)
            throw new ApplicationException("کاربر یافت نشد.");

        // تبدیل به DTO و بازگشت
        return new UserResponseDto
        {
            Id = currentUser.Id,
            Email = currentUser.Email,
            DisplayName = currentUser.DisplayName,
            Token = GenerateJwtToken(currentUser) // اختیاری: اگر نیاز به توکن جدید دارید
        };
    }
}