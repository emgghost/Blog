using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Blog.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IClaimProvider _claimProvider;
    private readonly IUserService _userService;

    public CurrentUserService(IClaimProvider claimProvider, IUserService userService)
    {
        _claimProvider = claimProvider;
        _userService = userService;
    }

    public string? UserId => _claimProvider.GetUserId();

    public string? UserName => _claimProvider.GetClaimValue(ClaimTypes.Name);

    public string? UserEmail => _claimProvider.GetUserEmail();

    public bool IsAuthenticated => _claimProvider.IsAuthenticated();

    public IEnumerable<string> Roles => _claimProvider.GetUserRoles();

    public async Task<UserDto?> GetCurrentUserAsync()
    {
        if (!IsAuthenticated || string.IsNullOrEmpty(UserEmail))
            return null;

        try
        {
            return await _userService.GetByEmailAsync(UserEmail);
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }
}
