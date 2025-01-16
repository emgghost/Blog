namespace Blog.Application.DTOs;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Excerpt { get; set; }
    public string? FeaturedImageUrl { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = null!;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public List<string> Tags { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string Status { get; set; } = null!;
    public int ViewCount { get; set; }
    public int CommentCount { get; set; }
}

public class PostCreateDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? Excerpt { get; set; }
    public string? FeaturedImageUrl { get; set; }
    public int CategoryId { get; set; }
    public List<int> TagIds { get; set; } = new();
    public string Status { get; set; } = "draft";
}

public class PostUpdateDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Excerpt { get; set; }
    public string? FeaturedImageUrl { get; set; }
    public int? CategoryId { get; set; }
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
