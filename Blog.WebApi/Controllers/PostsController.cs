using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var posts = await _postService.GetAllAsync(page, pageSize);
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetPost(int id)
    {
        try
        {
            var post = await _postService.GetByIdAsync(id);
            return Ok(post);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<PostDto>> CreatePost(CreatePostDto createPostDto)
    {
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
        var post = await _postService.CreateAsync(createPostDto, userId);
        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<PostDto>> UpdatePost(int id, UpdatePostDto updatePostDto)
    {
        try
        {
            var post = await _postService.UpdateAsync(id, updatePostDto);
            return Ok(post);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        try
        {
            await _postService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("author/{authorId}")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsByAuthor(
        int authorId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var posts = await _postService.GetByAuthorIdAsync(authorId, page, pageSize);
        return Ok(posts);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsByCategory(
        int categoryId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var posts = await _postService.GetByCategoryAsync(categoryId, page, pageSize);
        return Ok(posts);
    }

    [HttpGet("tag/{tagId}")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsByTag(
        int tagId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var posts = await _postService.GetByTagAsync(tagId, page, pageSize);
        return Ok(posts);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<PostDto>>> SearchPosts(
        [FromQuery] string query,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var posts = await _postService.SearchAsync(query, page, pageSize);
        return Ok(posts);
    }
}
