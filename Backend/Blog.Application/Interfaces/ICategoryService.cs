using Blog.Application.DTOs;

namespace Blog.Application.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
    Task<CategoryDto?> GetByIdAsync(int id);
    Task<CategoryDto> CreateAsync(CategoryCreateDto createDto);
    Task<CategoryDto?> UpdateAsync(int id, CategoryCreateDto updateDto);
    Task DeleteAsync(int id);
}