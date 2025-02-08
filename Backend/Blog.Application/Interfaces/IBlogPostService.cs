using Blog.Application.DTOs;
using Blog.Domain.Entities;

namespace Blog.Application.Interfaces;

public interface IBlogPostService
{
    Task<BlogPost> CreatePostAsync(BlogPostCreateDto createDto);
    Task<BlogPost> UpdatePostAsync(int id, BlogPostUpdateDto updateDto);
    Task DeletePostAsync(int id);
    Task<BlogPostDto> GetPostBySlugAsync(string slug);
    Task<List<BlogPostDto>> GetAllPostsAsync();
}