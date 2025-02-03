using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(1000)]
    public string Content { get; set; }
    
    [Required]
    [StringLength(100)]
    public string AuthorName { get; set; }
    
    [EmailAddress]
    [StringLength(100)]
    public string? AuthorEmail { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsApproved { get; set; } = false; // برای تأیید نظرات
    
    // رابطه با BlogPost
    public int BlogPostId { get; set; }
    public BlogPost BlogPost { get; set; }
}