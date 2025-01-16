using Blog.Application.DTOs;

namespace Blog.Application.Interfaces;

public interface ICategoryService
{
    Task<CategoryDto> GetByIdAsync(int id);
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto);
    Task<CategoryDto> UpdateAsync(int id, UpdateCategoryDto updateCategoryDto);
    Task DeleteAsync(int id);
    Task<CategoryDto> GetBySlugAsync(string slug);
}
