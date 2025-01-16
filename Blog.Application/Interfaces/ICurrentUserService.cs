namespace Blog.Application.Interfaces;

/// <summary>
/// Service for accessing current user information from the HTTP context
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the current user's ID
    /// </summary>
    string? UserId { get; }

    /// <summary>
    /// Gets the current user's username
    /// </summary>
    string? UserName { get; }

    /// <summary>
    /// Gets the current user's email address
    /// </summary>
    string? UserEmail { get; }

    /// <summary>
    /// Gets a value indicating whether the current user is authenticated
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Gets the roles assigned to the current user
    /// </summary>
    IEnumerable<string> Roles { get; }
}
