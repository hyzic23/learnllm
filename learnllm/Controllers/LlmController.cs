using learnllm.Models;
using learnllm.Services;
using Microsoft.AspNetCore.Mvc;

namespace learnllm.Controllers;

[ApiController]
[Route("[controller]")]
public class LlmController : ControllerBase
{
    private readonly ILlmService _llmService;

    public LlmController(ILlmService llmService)
    {
        _llmService = llmService;
    }

    // GET
    [HttpGet(Name = "/chat")]
    public async Task<IActionResult> Chat()
    {
        var request = new ChatRequest
        {
            Message = "Explain Dependency Injection in .NET."
        };
        var response = await _llmService.ChatAsync(request);
        return Ok(response);
    }
}