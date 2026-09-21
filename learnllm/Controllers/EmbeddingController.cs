using learnllm.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace learnllm.Controllers;

[ApiController]
[Route("[controller]")]
public class EmbeddingController : ControllerBase
{
    private readonly IEmbeddingService embeddingService;
    const string text = "Employees receive 15 vacation days every year.";

    public EmbeddingController(IEmbeddingService embeddingService)
    {
        this.embeddingService = embeddingService;
    }

    // GET
    [HttpGet("{embedding}")]
    public async Task<IActionResult> Index(string requestText)
    {
        requestText = text;
        var embedding = await embeddingService.CreateEmbeddingAsync(requestText);
        return Ok(new
        {
            Text = requestText,
            Embedding = embedding
        });
    }
}