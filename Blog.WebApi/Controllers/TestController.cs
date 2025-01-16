using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    /// <summary>
    /// Gets a test message
    /// </summary>
    /// <returns>A test message</returns>
    [HttpGet]
    public ActionResult<string> Get()
    {
        return Ok("API is working!");
    }
}
