namespace Blog.Application.DTOs;

public class CreateCommentDto
{
    public string Content { get; set; } = null!;
    public int PostId { get; set; }
    public int? ParentId { get; set; }
}

public class UpdateCommentDto
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
