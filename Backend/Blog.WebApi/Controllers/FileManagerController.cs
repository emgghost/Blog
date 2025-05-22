using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileManagerController : Controller
{
    [Authorize]
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("فایلی انتخاب نشده است.");
    
        if (!file.ContentType.StartsWith("image/"))
            return BadRequest("فایل باید تصویر باشد.");
        
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);
    
        var uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
    
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    
        var imageUrl = $"/images/{uniqueFileName}";
        return Ok(new { ImageUrl = imageUrl });
    }
}