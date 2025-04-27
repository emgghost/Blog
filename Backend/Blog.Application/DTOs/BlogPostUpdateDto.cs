using System.ComponentModel.DataAnnotations;

namespace Blog.Application.DTOs;

public class BlogPostUpdateDto
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    [StringLength(500)]
    public string ImageUrl { get; set; }

    public List<int> CategoryIds { get; set; } = new();
    public List<int> TagIds { get; set; } = new();
    public bool AddToSlider { get; set; } = false;

}