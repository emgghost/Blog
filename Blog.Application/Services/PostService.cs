using AutoMapper;
using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Services;

public class PostService : IPostService
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public PostService(BlogDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PostDto> GetByIdAsync(int id)
    {
        var post = await _context.Posts
            .Include(p => p.Author)
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID {id} not found.");

        return _mapper.Map<PostDto>(post);
    }

    public async Task<IEnumerable<PostDto>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        var posts = await _context.Posts
            .Include(p => p.Author)
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }

    public async Task<PostDto> CreateAsync(CreatePostDto createPostDto, int authorId)
    {
        var post = _mapper.Map<Post>(createPostDto);
        post.AuthorId = authorId;
        post.CreatedAt = DateTime.UtcNow;
        
        if (createPostDto.CategoryIds?.Any() == true)
        {
            var categories = await _context.Categories
                .Where(c => createPostDto.CategoryIds.Contains(c.Id))
                .ToListAsync();
            foreach (var category in categories)
            {
                post.Categories.Add(category);
            }
        }

        if (createPostDto.TagIds?.Any() == true)
        {
            var tags = await _context.Tags
                .Where(t => createPostDto.TagIds.Contains(t.Id))
                .ToListAsync();
            foreach (var tag in tags)
            {
                post.Tags.Add(tag);
            }
        }

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(post.Id);
    }

    public async Task<PostDto> UpdateAsync(int id, UpdatePostDto updatePostDto)
    {
        var post = await _context.Posts
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID {id} not found.");

        _mapper.Map(updatePostDto, post);
        post.UpdatedAt = DateTime.UtcNow;

        if (updatePostDto.CategoryIds != null)
        {
            post.Categories.Clear();
            var categories = await _context.Categories
                .Where(c => updatePostDto.CategoryIds.Contains(c.Id))
                .ToListAsync();
            foreach (var category in categories)
            {
                post.Categories.Add(category);
            }
        }

        if (updatePostDto.TagIds != null)
        {
            post.Tags.Clear();
            var tags = await _context.Tags
                .Where(t => updatePostDto.TagIds.Contains(t.Id))
                .ToListAsync();
            foreach (var tag in tags)
            {
                post.Tags.Add(tag);
            }
        }

        await _context.SaveChangesAsync();
        return await GetByIdAsync(post.Id);
    }

    public async Task DeleteAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
            throw new KeyNotFoundException($"Post with ID {id} not found.");

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<PostDto>> GetByAuthorIdAsync(int authorId, int page = 1, int pageSize = 10)
    {
        var posts = await _context.Posts
            .Include(p => p.Author)
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Where(p => p.AuthorId == authorId)
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }

    public async Task<IEnumerable<PostDto>> GetByCategoryAsync(int categoryId, int page = 1, int pageSize = 10)
    {
        var posts = await _context.Posts
            .Include(p => p.Author)
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Where(p => p.Categories.Any(c => c.Id == categoryId))
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }

    public async Task<IEnumerable<PostDto>> GetByTagAsync(int tagId, int page = 1, int pageSize = 10)
    {
        var posts = await _context.Posts
            .Include(p => p.Author)
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Where(p => p.Tags.Any(t => t.Id == tagId))
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }

    public async Task<IEnumerable<PostDto>> SearchAsync(string query, int page = 1, int pageSize = 10)
    {
        var posts = await _context.Posts
            .Include(p => p.Author)
            .Include(p => p.Categories)
            .Include(p => p.Tags)
            .Where(p => p.Title.Contains(query) || p.Content.Contains(query))
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }
}
