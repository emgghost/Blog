using AutoMapper;
using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Services;

public class TagService : ITagService
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public TagService(BlogDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TagDto> GetByIdAsync(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
            throw new KeyNotFoundException($"Tag with ID {id} not found.");

        return _mapper.Map<TagDto>(tag);
    }

    public async Task<IEnumerable<TagDto>> GetAllAsync()
    {
        var tags = await _context.Tags
            .OrderBy(t => t.Name)
            .ToListAsync();

        return _mapper.Map<IEnumerable<TagDto>>(tags);
    }

    public async Task<TagDto> CreateAsync(CreateTagDto createTagDto)
    {
        var tag = _mapper.Map<Tag>(createTagDto);
        
        if (string.IsNullOrEmpty(tag.Slug))
        {
            tag.Slug = createTagDto.Name.ToLower().Replace(" ", "-");
        }

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        return _mapper.Map<TagDto>(tag);
    }

    public async Task<TagDto> UpdateAsync(int id, UpdateTagDto updateTagDto)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
            throw new KeyNotFoundException($"Tag with ID {id} not found.");

        _mapper.Map(updateTagDto, tag);
        
        if (!string.IsNullOrEmpty(updateTagDto.Name) && string.IsNullOrEmpty(updateTagDto.Slug))
        {
            tag.Slug = updateTagDto.Name.ToLower().Replace(" ", "-");
        }

        await _context.SaveChangesAsync();

        return _mapper.Map<TagDto>(tag);
    }

    public async Task DeleteAsync(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
            throw new KeyNotFoundException($"Tag with ID {id} not found.");

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();
    }

    public async Task<TagDto> GetBySlugAsync(string slug)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Slug == slug);
        if (tag == null)
            throw new KeyNotFoundException($"Tag with slug '{slug}' not found.");

        return _mapper.Map<TagDto>(tag);
    }

    public async Task<IEnumerable<TagDto>> GetPopularTagsAsync(int count = 10)
    {
        var popularTags = await _context.Tags
            .Include(t => t.Posts)
            .OrderByDescending(t => t.Posts.Count)
            .Take(count)
            .ToListAsync();

        return _mapper.Map<IEnumerable<TagDto>>(popularTags);
    }
}
