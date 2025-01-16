using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Entities;

public class Tag
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    
    public string? Slug { get; set; }
    
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
