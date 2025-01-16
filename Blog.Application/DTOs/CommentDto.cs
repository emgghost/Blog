namespace Blog.Application.DTOs;

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int PostId { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = null!;
    public string? AuthorAvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? ParentId { get; set; }
    public List<CommentDto> Replies { get; set; } = new();
}

public class CommentCreateDto
{
    public string Content { get; set; } = null!;
    public int PostId { get; set; }
    public int? ParentId { get; set; }
}

public class CommentUpdateDto
{
    public string Content { get; set; } = null!;
}

public class CommentListDto
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public string AuthorName { get; set; } = null!;
    public string? AuthorAvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? ParentId { get; set; }
    public int RepliesCount { get; set; }
}
