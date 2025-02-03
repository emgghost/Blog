using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Entities;

public class Tag
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    [StringLength(50)]
    public string Slug { get; set; } // مثال: "aspnet-core"
    
    // روابط
    public List<BlogPostTag> BlogPostTags { get; set; } = new();
}