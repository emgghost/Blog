using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    
    [Required]
    public string Content { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public int PostId { get; set; }
    public virtual Post Post { get; set; } = null!;
    
    public int AuthorId { get; set; }
    public virtual User Author { get; set; } = null!;
    
    public int? ParentCommentId { get; set; }
    public virtual Comment? ParentComment { get; set; }
    public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
}
