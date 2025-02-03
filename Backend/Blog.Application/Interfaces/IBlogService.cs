using Blog.Application.DTOs;

namespace Blog.Application.Interfaces;

public interface IBlogService
{
    Task<BlogPostReadDto> CreatePostAsync(BlogPostCreateDto createDto);
    Task<IEnumerable<BlogPostReadDto>> GetAllPostsAsync();
    Task<BlogPostReadDto> GetPostBySlugAsync(string slug);
}