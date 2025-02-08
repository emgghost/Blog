using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

// TagsController.cs
[Route("api/tags")]
[ApiController]
public class TagsController(ITagService tagService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TagDto>>> GetAll()
    {
        return Ok(await tagService.GetAllAsync());
    }

    [HttpPost]
    public async Task<ActionResult<TagDto>> Create([FromBody] TagCreateDto createDto)
    {
        var tag = await tagService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = tag.Id }, tag);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TagDto>> GetById(int id)
    {
        var tag = await tagService.GetByIdAsync(id);
        if (tag == null)
        {
            return NotFound();
        }
        return Ok(tag);
    }
    
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<TagDto>> Update(int id, [FromBody] TagCreateDto updateDto)
    {
        var tag = await tagService.UpdateAsync(id, updateDto);
        if (tag == null)
        {
            return NotFound();
        }
        return Ok(tag);
    }
    
        
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await tagService.DeleteAsync(id);
        return NoContent();
    }
}