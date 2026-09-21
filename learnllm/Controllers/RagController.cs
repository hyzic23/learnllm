using learnllm.Models;
using learnllm.Services;
using Microsoft.AspNetCore.Mvc;

namespace learnllm.Controllers;

[ApiController]
[Route("[controller]")]
public class RagController : ControllerBase
{
    private readonly IRagService _ragService;

    public RagController(RagService ragService)
    {
        _ragService = ragService;
    }

    // GET
    [HttpGet("ask")]
    public async Task<IActionResult> Index(AskQuestionRequest request)
    {
        var response = await _ragService.AskAsync(request);
        return Ok(response);
    }
}