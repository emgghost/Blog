namespace Blog.Application.Interfaces;

/// <summary>
/// Service for handling date and time operations
/// </summary>
public interface IDateTimeService
{
    /// <summary>
    /// Gets the current UTC date and time
    /// </summary>
    DateTime Now { get; }
}
