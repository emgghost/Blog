using System.Security.Claims;

namespace Blog.Application.Interfaces;

/// <summary>
/// Provides access to user claims and common claim-based operations
/// </summary>
public interface IClaimProvider
{
    /// <summary>
    /// Gets the current ClaimsPrincipal
    /// </summary>
    ClaimsPrincipal? User { get; }
    
    /// <summary>
    /// Gets a specific claim value by type
    /// </summary>
    /// <param name="claimType">The type of claim to retrieve</param>
    /// <returns>The claim value if found, null otherwise</returns>
    string? GetClaimValue(string claimType);
    
    /// <summary>
    /// Gets all claims of a specific type
    /// </summary>
    /// <param name="claimType">The type of claims to retrieve</param>
    /// <returns>An enumerable of claim values</returns>
    IEnumerable<string> GetClaimValues(string claimType);
    
    /// <summary>
    /// Checks if the current user has a specific claim
    /// </summary>
    /// <param name="claimType">The type of claim to check</param>
    /// <param name="claimValue">The value of the claim to check</param>
    /// <returns>True if the user has the specified claim, false otherwise</returns>
    bool HasClaim(string claimType, string claimValue);
    
    /// <summary>
    /// Gets the current user's ID from claims
    /// </summary>
    /// <returns>The user ID if present, null otherwise</returns>
    string? GetUserId();
    
    /// <summary>
    /// Gets the current user's email from claims
    /// </summary>
    /// <returns>The user email if present, null otherwise</returns>
    string? GetUserEmail();
    
    /// <summary>
    /// Gets the current user's roles from claims
    /// </summary>
    /// <returns>An enumerable of role names</returns>
    IEnumerable<string> GetUserRoles();
    
    /// <summary>
    /// Checks if the current user is in a specific role
    /// </summary>
    /// <param name="role">The role to check</param>
    /// <returns>True if the user is in the specified role, false otherwise</returns>
    bool IsInRole(string role);
    
    /// <summary>
    /// Checks if the current user is authenticated
    /// </summary>
    /// <returns>True if the user is authenticated, false otherwise</returns>
    bool IsAuthenticated();
}
