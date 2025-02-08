using System.Security.Claims;
using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.WebApi.Controllers;

[Route("api/blog/[controller]")]
[ApiController]
public class BlogPostsController : ControllerBase
{
    private readonly IBlogPostService _blogPostService;

    public BlogPostsController(IBlogPostService blogPostService)
    {
        _blogPostService = blogPostService;
    }

    [HttpPost]
    public async Task<ActionResult<BlogPostDto>> CreatePost([FromBody] BlogPostCreateDto createDto)
    {
        var post = await _blogPostService.CreatePostAsync(createDto);
        return CreatedAtAction(nameof(GetPostBySlug), new { slug = post.Slug }, post);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] BlogPostUpdateDto updateDto)
    {
        await _blogPostService.UpdatePostAsync(id, updateDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        await _blogPostService.DeletePostAsync(id);
        return NoContent();
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<BlogPostDto>> GetPostBySlug(string slug)
    {
        var post = await _blogPostService.GetPostBySlugAsync(slug);
        if (post == null)
            return NotFound();
        
        return Ok(post);
    }

    [HttpGet]
    public async Task<ActionResult<List<BlogPostDto>>> GetAllPosts()
    {
        var posts = await _blogPostService.GetAllPostsAsync();
        return Ok(posts);
    }
    
}