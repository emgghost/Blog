namespace Blog.Application.DTOs;

public class TagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public int PostCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class TagCreateDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

public class TagUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class TagListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public int PostCount { get; set; }
}

public class PostTagDto
{
    public int PostId { get; set; }
    public int TagId { get; set; }
}
