using System.ComponentModel.DataAnnotations;

namespace Blog.Application.DTOs;

public class CategoryCreateDto
{
    [Required]
    public string Name { get; set; }
}