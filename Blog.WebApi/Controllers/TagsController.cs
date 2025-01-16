using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetTags()
    {
        var tags = await _tagService.GetAllAsync();
        return Ok(tags);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TagDto>> GetTag(int id)
    {
        try
        {
            var tag = await _tagService.GetByIdAsync(id);
            return Ok(tag);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("slug/{slug}")]
    public async Task<ActionResult<TagDto>> GetTagBySlug(string slug)
    {
        try
        {
            var tag = await _tagService.GetBySlugAsync(slug);
            return Ok(tag);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("popular")]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetPopularTags([FromQuery] int count = 10)
    {
        var tags = await _tagService.GetPopularTagsAsync(count);
        return Ok(tags);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<TagDto>> CreateTag(CreateTagDto createTagDto)
    {
        var tag = await _tagService.CreateAsync(createTagDto);
        return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<TagDto>> UpdateTag(int id, UpdateTagDto updateTagDto)
    {
        try
        {
            var tag = await _tagService.UpdateAsync(id, updateTagDto);
            return Ok(tag);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(int id)
    {
        try
        {
            await _tagService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
