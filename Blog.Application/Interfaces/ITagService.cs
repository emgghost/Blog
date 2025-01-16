using Blog.Application.DTOs;

namespace Blog.Application.Interfaces;

public interface ITagService
{
    Task<TagDto> GetByIdAsync(int id);
    Task<IEnumerable<TagDto>> GetAllAsync();
    Task<TagDto> CreateAsync(CreateTagDto createTagDto);
    Task<TagDto> UpdateAsync(int id, UpdateTagDto updateTagDto);
    Task DeleteAsync(int id);
    Task<TagDto> GetBySlugAsync(string slug);
    Task<IEnumerable<TagDto>> GetPopularTagsAsync(int count = 10);
}
