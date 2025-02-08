using System.ComponentModel.DataAnnotations;

namespace Blog.Application.DTOs;

public class TagCreateDto
{
    [Required]
    public string Name { get; set; }
}