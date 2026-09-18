using learnllm.Models;
using learnllm.Services;
using Microsoft.AspNetCore.Mvc;

namespace learnllm.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly ILlmService _llmService;

    public ChatController(ILlmService llmService)
    {
        _llmService = llmService;
    }

    // GET
    [HttpGet(Name = "/")]
    public async Task<IActionResult> Index()
    {
        var request = new ChatRequest
        {
            Message = "Explain Dependency Injection in .NET."
        };
        var response = await _llmService.ChatAsync(request);
        return Ok(response);
    }
}