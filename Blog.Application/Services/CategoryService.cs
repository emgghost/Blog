using AutoMapper;
using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public CategoryService(BlogDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoryDto> GetByIdAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            throw new KeyNotFoundException($"Category with ID {id} not found.");

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _context.Categories
            .OrderBy(c => c.Name)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto)
    {
        var category = _mapper.Map<Category>(createCategoryDto);
        
        if (string.IsNullOrEmpty(category.Slug))
        {
            category.Slug = createCategoryDto.Name.ToLower().Replace(" ", "-");
        }

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> UpdateAsync(int id, UpdateCategoryDto updateCategoryDto)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            throw new KeyNotFoundException($"Category with ID {id} not found.");

        _mapper.Map(updateCategoryDto, category);
        
        if (!string.IsNullOrEmpty(updateCategoryDto.Name) && string.IsNullOrEmpty(updateCategoryDto.Slug))
        {
            category.Slug = updateCategoryDto.Name.ToLower().Replace(" ", "-");
        }

        await _context.SaveChangesAsync();

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            throw new KeyNotFoundException($"Category with ID {id} not found.");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<CategoryDto> GetBySlugAsync(string slug)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Slug == slug);
        if (category == null)
            throw new KeyNotFoundException($"Category with slug '{slug}' not found.");

        return _mapper.Map<CategoryDto>(category);
    }
}
