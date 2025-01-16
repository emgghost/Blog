namespace Blog.Application.DTOs;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? FeaturedImage { get; set; }
    public string? Excerpt { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public UserDto Author { get; set; } = null!;
    public ICollection<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    public ICollection<TagDto> Tags { get; set; } = new List<TagDto>();
}

public class CreatePostDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? Excerpt { get; set; }
    public string? FeaturedImageUrl { get; set; }
    public List<int> CategoryIds { get; set; }
    public List<int> TagIds { get; set; } = new();
    public string Status { get; set; } = "draft";
}

public class UpdatePostDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Excerpt { get; set; }
    public string? FeaturedImageUrl { get; set; }
    public List<int> CategoryIds { get; set; }
    public List<int>? TagIds { get; set; }
    public string? Status { get; set; }
}

public class PostListDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Excerpt { get; set; }
    public string? FeaturedImageUrl { get; set; }
    public string AuthorName { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string Status { get; set; } = null!;
    public int ViewCount { get; set; }
    public int CommentCount { get; set; }
}
