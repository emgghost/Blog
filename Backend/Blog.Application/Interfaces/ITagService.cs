using Blog.Application.DTOs;

namespace Blog.Application.Interfaces;

public interface ITagService
{
    Task<List<TagDto>> GetAllAsync();
    Task<TagDto> GetByIdAsync(int id);
    Task<TagDto> CreateAsync(TagCreateDto createDto);
    Task<TagDto> UpdateAsync(int id, TagCreateDto updateDto);
    Task DeleteAsync(int id);
}