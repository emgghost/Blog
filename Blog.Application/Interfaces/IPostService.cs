using Blog.Application.DTOs;

namespace Blog.Application.Interfaces;

public interface IPostService
{
    Task<PostDto> GetByIdAsync(int id);
    Task<IEnumerable<PostDto>> GetAllAsync(int page = 1, int pageSize = 10);
    Task<PostDto> CreateAsync(CreatePostDto createPostDto, int authorId);
    Task<PostDto> UpdateAsync(int id, UpdatePostDto updatePostDto);
    Task DeleteAsync(int id);
    Task<IEnumerable<PostDto>> GetByAuthorIdAsync(int authorId, int page = 1, int pageSize = 10);
    Task<IEnumerable<PostDto>> GetByCategoryAsync(int categoryId, int page = 1, int pageSize = 10);
    Task<IEnumerable<PostDto>> GetByTagAsync(int tagId, int page = 1, int pageSize = 10);
    Task<IEnumerable<PostDto>> SearchAsync(string query, int page = 1, int pageSize = 10);
}
