namespace Blog.Application.DTOs;

// برای نمایش پست به کاربر
public class BlogPostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Slug { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    // اطلاعات نویسنده
    public AuthorDto? Author { get; set; }
    
    public List<CategoryDto> Categories { get; set; }
    public List<TagDto> Tags { get; set; }
    public List<CommentDto> Comments { get; set; }
}

public class AuthorDto
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string? ProfileImageUrl { get; set; }
}