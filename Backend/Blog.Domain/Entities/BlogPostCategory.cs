namespace Blog.Domain.Entities;

// برای رابطه BlogPost و Category
public class BlogPostCategory
{
    public int BlogPostId { get; set; }
    public BlogPost BlogPost { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

// برای رابطه BlogPost و Tag
public class BlogPostTag
{
    public int BlogPostId { get; set; }
    public BlogPost BlogPost { get; set; }
    
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}