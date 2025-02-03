using Blog.Domain.Entities;

namespace Blog.Application.DTOs;

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string AuthorName { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsApproved { get; set; }
    
    // برای نمایش در API (بدون اطلاعات حساس)
    public static CommentDto FromComment(Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            AuthorName = comment.AuthorName,
            CreatedAt = comment.CreatedAt,
            IsApproved = comment.IsApproved
        };
    }
}