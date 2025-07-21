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
    public string? SliderImageUrl { get; set; } // آدرس عکس اسلایدر در سرور
    public List<int> CategoryIds { get; set; } = new();
    public List<int> TagIds { get; set; } = new();
    public bool AddToSlider { get; set; } = false;

}