using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Application.Services;

public class UserService : IUserService
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserService(BlogDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {id} not found.");

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            throw new KeyNotFoundException($"User with email {email} not found.");

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateAsync(UserCreateDto UserCreateDto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == UserCreateDto.Email))
            throw new InvalidOperationException("Email already exists.");

        if (await _context.Users.AnyAsync(u => u.Username == UserCreateDto.Username))
            throw new InvalidOperationException("Username already exists.");

        var user = _mapper.Map<User>(UserCreateDto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(UserCreateDto.Password);
        user.CreatedAt = DateTime.UtcNow;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateAsync(int id, UpdateUserDto updateUserDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {id} not found.");

        if (updateUserDto.Email != null && updateUserDto.Email != user.Email)
        {
            if (await _context.Users.AnyAsync(u => u.Email == updateUserDto.Email))
                throw new InvalidOperationException("Email already exists.");
        }

        if (updateUserDto.Username != null && updateUserDto.Username != user.Username)
        {
            if (await _context.Users.AnyAsync(u => u.Username == updateUserDto.Username))
                throw new InvalidOperationException("Username already exists.");
        }

        _mapper.Map(updateUserDto, user);
        
        if (!string.IsNullOrEmpty(updateUserDto.Password))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password);
        }

        user.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {id} not found.");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password.");

        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        var token = GenerateJwtToken(user);
        return token;
    }

    public async Task<UserDto> GetCurrentUserAsync()
    {
        // This method should be implemented based on your authentication context
        throw new NotImplementedException();
    }

    private string GenerateJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured"));
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
