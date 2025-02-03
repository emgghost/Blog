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
    private readonly IBlogService _blogService;

    public BlogPostsController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    [HttpPost]
    public async Task<ActionResult<BlogPostReadDto>> CreatePost(BlogPostCreateDto createDto)
    {
        var post = await _blogService.CreatePostAsync(createDto);
        return CreatedAtAction(nameof(GetPostBySlug), new { slug = post.Slug }, post);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BlogPostReadDto>>> GetAllPosts()
    {
        var posts = await _blogService.GetAllPostsAsync();
        return Ok(posts);
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<BlogPostReadDto>> GetPostBySlug(string slug)
    {
        var post = await _blogService.GetPostBySlugAsync(slug);
        if (post == null) return NotFound();
        return Ok(post);
    }
}