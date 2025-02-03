using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Domain.Entities;

public class BlogPost
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; }
    
    [Required]
    [Column(TypeName = "ntext")]
    public string Content { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Slug { get; set; } // مثال: "my-first-post"
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // روابط
    public List<BlogPostCategory> BlogPostCategories { get; set; } = new();
    public List<BlogPostTag> BlogPostTags { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
    
    // نویسنده (اگر نیاز به احراز هویت دارید)
    public string? AuthorId { get; set; }
    public ApplicationUser? Author { get; set; }
}