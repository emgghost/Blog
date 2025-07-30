using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Blog.Application.Utilities;
using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CategoryService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return _mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return null;
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateAsync(CategoryCreateDto createDto)
    {
        var category = _mapper.Map<Category>(createDto);
        category.Slug = Slug.GenerateSlug(createDto.Name);
        
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto?> UpdateAsync(int id, CategoryCreateDto updateDto)   
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return null;
        
        category.Name = updateDto.Name;
        category.Slug = Slug.GenerateSlug(updateDto.Name);
            
        await _context.SaveChangesAsync();
        
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            throw new Exception("دسته‌بندی مورد نظر یافت نشد.");
        _context.Categories.Remove(category); 
        await _context.SaveChangesAsync();
        return;
    }

    public async Task<GetPostByCategoryDto> GetPostByCategoryAsync(string slug)
    {
        var tag = await _context.Categories
            .Include(t => t.BlogPostCategories)
            .ThenInclude(pt => pt.BlogPost)
            .ThenInclude(p => p.BlogPostCategories)
            .ThenInclude(pc => pc.Category)
            .SingleOrDefaultAsync(x => x.Slug == slug);
        
        var res = _mapper.Map<GetPostByCategoryDto>(tag);
        res.BlogPosts = _mapper.Map<List<BlogPostDto>>(tag.BlogPostCategories.Select(x => x.BlogPost));
        return res;
    }

    // سایر متدها (Update, Delete, GetById)
}