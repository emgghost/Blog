namespace Blog.Domain.Entities;

using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    // اطلاعات اضافی کاربر (اختیاری)
    public string? DisplayName { get; set; }
    public string? ProfileImageUrl { get; set; }
    
    // رابطه با پست‌ها
    public List<BlogPost> BlogPosts { get; set; } = new();
}