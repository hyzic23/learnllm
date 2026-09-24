using learnllm.Services;
using Microsoft.AspNetCore.Mvc;

namespace learnllm.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IDocumentProcessor _processor;
    
    public DocumentController(IDocumentProcessor processor)
    {
        _processor = processor;
    }
    
    [HttpPost("/")]
    public async Task<IActionResult> Index(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        var text = await reader.ReadToEndAsync();
        var count = await _processor.ProcessAsync(file.FileName, text);
        return Ok(new
        {
            FileName = file.FileName,
            ChunkCreated = count
        });
    }
}