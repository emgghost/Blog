namespace Blog.Application.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Slug { get; set; }
}

public class CreateCategoryDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Slug { get; set; }
}

public class UpdateCategoryDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Slug { get; set; }
}

public class CategoryListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public int PostCount { get; set; }
}
