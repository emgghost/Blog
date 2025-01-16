namespace Blog.Application.DTOs;

public class TagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Slug { get; set; }
}

public class CreateTagDto
{
    public string Name { get; set; } = null!;
    public string? Slug { get; set; }
}

public class UpdateTagDto
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
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
