using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Entities;

public class Post
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = null!;
    
    [Required]
    public string Content { get; set; } = null!;
    
    public string? FeaturedImage { get; set; }
    public string? Excerpt { get; set; }
    
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public int AuthorId { get; set; }
    public virtual User Author { get; set; } = null!;
    
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
