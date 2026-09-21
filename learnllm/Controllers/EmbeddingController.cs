using learnllm.Dtos;
using learnllm.Services;
using Microsoft.AspNetCore.Mvc;

namespace learnllm.Controllers;

[ApiController]
[Route("[controller]")]
public class EmbeddingController : ControllerBase
{
    private readonly IEmbeddingService embeddingService;
    private const string Text = "Employees receive 15 vacation days every year.";

    public EmbeddingController(IEmbeddingService embeddingService)
    {
        this.embeddingService = embeddingService;
    }

    // GET
    [HttpGet("embedding/{requestText}")]
    public async Task<IActionResult> Index(string requestText)
    {
        requestText = Text;
        var embedding = await embeddingService.CreateEmbeddingAsync(requestText);
        return Ok(new LlmDto.EmbeddingResponse(requestText, embedding));
    }
}