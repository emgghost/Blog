using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    public string? Slug { get; set; }
    
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
