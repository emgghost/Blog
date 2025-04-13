using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Blog.Application.Utilities;
using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Services;

public class TagService : ITagService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TagService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TagDto>> GetAllAsync()
    {
        var categories = await _context.Tags.ToListAsync();
        return _mapper.Map<List<TagDto>>(categories);
    }

    public async Task<TagDto> GetByIdAsync(int id)
    {
        var Tag = await _context.Tags.FindAsync(id);
        if (Tag == null)
            throw new Exception("تگ مورد نظر یافت نشد.");
        return _mapper.Map<TagDto>(Tag);
    }

    public async Task<TagDto> CreateAsync(TagCreateDto createDto)
    {
        var Tag = _mapper.Map<Tag>(createDto);
        Tag.Slug = Slug.GenerateSlug(createDto.Name);
        
        _context.Tags.Add(Tag);
        await _context.SaveChangesAsync();
        return _mapper.Map<TagDto>(Tag);
    }

    public async Task<TagDto> UpdateAsync(int id, TagCreateDto updateDto)   
    {
        var Tag = await _context.Tags.FindAsync(id);
        if (Tag == null)
            throw new Exception("تگ مورد نظر یافت نشد.");
        
        Tag.Name = updateDto.Name;
        Tag.Slug = Slug.GenerateSlug(updateDto.Name);
            
        await _context.SaveChangesAsync();
        
        return _mapper.Map<TagDto>(Tag);
    }

    public async Task DeleteAsync(int id)
    {
        var Tag = await _context.Tags.FindAsync(id);
        if (Tag == null)
            throw new Exception("تگ مورد نظر یافت نشد.");
        _context.Tags.Remove(Tag); 
        await _context.SaveChangesAsync();
        return;
    }

    public async Task<GetPostByTagDto> GetPostByTagAsync(string slug)
    {
        var tag = await _context.Tags
            .Include(t => t.BlogPostTags)
            .ThenInclude(pt => pt.BlogPost)
            .ThenInclude(p => p.BlogPostCategories)
            .ThenInclude(pc => pc.Category)
            .Include(t => t.BlogPostTags)
            .ThenInclude(pt => pt.BlogPost)
            .SingleOrDefaultAsync(x => x.Slug == slug);
        
        var res = _mapper.Map<GetPostByTagDto>(tag);
        res.BlogPosts = _mapper.Map<List<BlogPostDto>>(tag.BlogPostTags.Select(x => x.BlogPost));
        return res;
    }

    // سایر متدها (Update, Delete, GetById)
}