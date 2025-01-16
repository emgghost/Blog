namespace Blog.Application.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public int PostCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CategoryCreateDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

public class CategoryUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class CategoryListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public int PostCount { get; set; }
}
