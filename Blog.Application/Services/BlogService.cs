using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Services;

public class BlogService : IBlogService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public BlogService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BlogPostReadDto> CreatePostAsync(BlogPostCreateDto createDto)
    {
        var post = _mapper.Map<BlogPost>(createDto);
        
        // افزودن دسته‌بندی‌ها
        foreach (var categoryId in createDto.CategoryIds)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) throw new KeyNotFoundException("Category not found");
            post.BlogPostCategories.Add(new BlogPostCategory { Category = category });
        }

        // افزودن تگ‌ها (مشابه بالا)

        _context.BlogPosts.Add(post);
        await _context.SaveChangesAsync();

        return _mapper.Map<BlogPostReadDto>(post);
    }

    public async Task<IEnumerable<BlogPostReadDto>> GetAllPostsAsync()
    {
        var posts = await _context.BlogPosts
            .Include(p => p.BlogPostCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.BlogPostTags)
            .ThenInclude(pt => pt.Tag)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BlogPostReadDto>>(posts);
    }

    public Task<BlogPostReadDto> GetPostBySlugAsync(string slug)
    {
        throw new NotImplementedException();
    }

    // سایر متدها...
}