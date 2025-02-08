using AutoMapper;
using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Blog.Application.Utilities;
using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Services;

public class BlogPostService : IBlogPostService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public BlogPostService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BlogPost> CreatePostAsync(BlogPostCreateDto createDto)
    {
        var post = _mapper.Map<BlogPost>(createDto);
        post.Slug = Slug.GenerateSlug(createDto.Title); // تابع تولید Slug
        
        _context.BlogPosts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<BlogPost> UpdatePostAsync(int id, BlogPostUpdateDto updateDto)
    {
        var post = await _context.BlogPosts.FindAsync(id);
        if (post == null)
            throw new KeyNotFoundException("پست یافت نشد.");
        
        _mapper.Map(updateDto, post);
        post.UpdatedAt = DateTime.UtcNow;
        post.Slug = Slug.GenerateSlug(updateDto.Title); // آپدیت Slug
        
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task DeletePostAsync(int id)
    {
        var post = await _context.BlogPosts.FindAsync(id);
        if (post == null)
            throw new KeyNotFoundException("پست یافت نشد.");
        
        _context.BlogPosts.Remove(post);
        await _context.SaveChangesAsync();
    }

    public async Task<BlogPostDto> GetPostBySlugAsync(string slug)
    {
        var post = await _context.BlogPosts
            .Include(p => p.BlogPostCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.BlogPostTags)
            .ThenInclude(pt => pt.Tag)
            .Include(p => p.Author)
            .FirstOrDefaultAsync(p => p.Slug == slug);
        
        return _mapper.Map<BlogPostDto>(post);
    }

    public async Task<List<BlogPostDto>> GetAllPostsAsync()
    {
        var posts = await _context.BlogPosts
            .Include(p => p.BlogPostCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.BlogPostTags)
            .ThenInclude(pt => pt.Tag)
            .Include(p => p.Author)
            .ToListAsync();
        
        return _mapper.Map<List<BlogPostDto>>(posts);
    }

    
}