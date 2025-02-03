using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Entities;


public class Category
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(50)]
    public string Slug { get; set; } // مثال: "programming"

    // روابط
    public List<BlogPostCategory> BlogPostCategories { get; set; } = new();
}
