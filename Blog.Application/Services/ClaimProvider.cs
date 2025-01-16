using System.Security.Claims;
using Blog.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Blog.Application.Services;

/// <summary>
/// Default implementation of IClaimProvider that uses HttpContext to access claims
/// </summary>
public class ClaimProvider : IClaimProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public string? GetClaimValue(string claimType)
    {
        return User?.FindFirstValue(claimType);
    }

    public IEnumerable<string> GetClaimValues(string claimType)
    {
        return User?.Claims
            .Where(c => c.Type == claimType)
            .Select(c => c.Value)
            ?? Enumerable.Empty<string>();
    }

    public bool HasClaim(string claimType, string claimValue)
    {
        return User?.HasClaim(c => c.Type == claimType && c.Value == claimValue) ?? false;
    }

    public string? GetUserId()
    {
        return GetClaimValue(ClaimTypes.NameIdentifier);
    }

    public string? GetUserEmail()
    {
        return GetClaimValue(ClaimTypes.Email);
    }

    public IEnumerable<string> GetUserRoles()
    {
        return GetClaimValues(ClaimTypes.Role);
    }

    public bool IsInRole(string role)
    {
        return User?.IsInRole(role) ?? false;
    }

    public bool IsAuthenticated()
    {
        return User?.Identity?.IsAuthenticated ?? false;
    }
}
