using System.ComponentModel.DataAnnotations;

namespace Blog.Application.DTOs;

// برای ایجاد پست جدید
public class BlogPostCreateDto
{
    [Required(ErrorMessage = "عنوان پست الزامی است.")]
    [StringLength(200, ErrorMessage = "عنوان نباید بیش از 200 کاراکتر باشد.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "محتوای پست الزامی است.")]
    public string Content { get; set; }

    [Required(ErrorMessage = "حداقل یک دسته‌بندی انتخاب کنید.")]
    public List<int> CategoryIds { get; set; } = new();

    public List<int> TagIds { get; set; } = new();
    
    public string ImageUrl { get; set; } // آدرس عکس در سرور
    
    public string? AuthorId { get; set; }

}