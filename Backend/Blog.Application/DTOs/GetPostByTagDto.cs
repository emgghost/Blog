namespace Blog.Application.DTOs;

public class GetPostByTagDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public ICollection<BlogPostDto> BlogPosts { get; set; }
}