namespace Blog.Application.DTOs;

public class BlogPostReadDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Slug { get; set; }
    public List<CategoryReadDto> Categories { get; set; }
    public List<TagReadDto> Tags { get; set; }
}