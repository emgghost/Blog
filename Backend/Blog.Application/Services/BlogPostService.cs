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
    var post = await _context.BlogPosts
        .Include(p => p.BlogPostCategories)
        .ThenInclude(pc => pc.Category)
        .Include(p => p.BlogPostTags)
        .ThenInclude(pt => pt.Tag)
        .FirstOrDefaultAsync(p => p.Id == id);

    if (post == null)
        throw new KeyNotFoundException("پست یافت نشد.");

    // 更新主博客文章信息
    _mapper.Map(updateDto, post);
    post.UpdatedAt = DateTime.UtcNow;
    post.Slug = Slug.GenerateSlug(updateDto.Title); // 更新 Slug

    // 更新分类
    if (updateDto.CategoryIds.Count > 0)
    {
        var existingCategoryIds = post.BlogPostCategories.Select(c => c.CategoryId).ToHashSet();
        var updatedCategoryIds = updateDto.CategoryIds.ToHashSet();

        // 删除不再存在的分类
        var categoriesToRemove = post.BlogPostCategories.Where(c => !updatedCategoryIds.Contains(c.CategoryId)).ToList();
        foreach (var category in categoriesToRemove)
        {
            _context.BlogPostCategories.Remove(category);
        }

        // 添加新的分类
        var categoriesToAdd = updateDto.CategoryIds.Where(c => !existingCategoryIds.Contains(c)).Select(categoryId => new BlogPostCategory
        {
            BlogPostId = post.Id,
            CategoryId = categoryId
        });
        await _context.BlogPostCategories.AddRangeAsync(categoriesToAdd);
    }
    else
    {
        // 删除所有分类
        var categoriesToRemove = post.BlogPostCategories.ToList();
        _context.BlogPostCategories.RemoveRange(categoriesToRemove);
    }
    // 更新标签
    if (updateDto.TagIds.Count > 0)
    {
        var existingTagIds = post.BlogPostTags.Select(t => t.TagId).ToHashSet();
        var updatedTagIds = updateDto.TagIds.ToHashSet();

        // 删除不再存在的标签
        var tagsToRemove = post.BlogPostTags.Where(t => !updatedTagIds.Contains(t.TagId)).ToList();
        foreach (var tag in tagsToRemove)
        {
            _context.BlogPostTags.Remove(tag);
        }

        // 添加新的标签
        var tagsToAdd = updateDto.TagIds.Where(t => !existingTagIds.Contains(t)).Select(tagId => new BlogPostTag
        {
            BlogPostId = post.Id,
            TagId = tagId
        });
        await _context.BlogPostTags.AddRangeAsync(tagsToAdd);
    }
    else
    {
        // 删除所有标签
        var tagsToRemove = post.BlogPostTags.ToList();
        _context.BlogPostTags.RemoveRange(tagsToRemove);
    }

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

        if (post != null)
        {
            post.ReadCount++;
            await _context.SaveChangesAsync();
        }
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