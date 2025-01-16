using Blog.Application.DTOs;

namespace Blog.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> GetByIdAsync(int id);
    Task<UserDto> GetByEmailAsync(string email);
    Task<UserDto> CreateAsync(CreateUserDto CreateUserDto);
    Task<UserDto> UpdateAsync(int id, UpdateUserDto updateUserDto);
    Task DeleteAsync(int id);
    Task<string> LoginAsync(LoginDto loginDto);
    Task<UserDto> GetCurrentUserAsync();
}
