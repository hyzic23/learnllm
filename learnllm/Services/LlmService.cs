using learnllm.Config;
using learnllm.Models;
using Microsoft.Extensions.Options;

namespace learnllm.Services;

public class LlmService : ILlmService
{
    private readonly LlmOptions _llmOptions;

    public LlmService(IOptions<LlmOptions> llmOptions)
    {
        _llmOptions = llmOptions.Value;
    }

    public async Task<ChatResponse> ChatAsync(ChatRequest request)
    {
        Console.WriteLine($"Using model : {_llmOptions.Model }");
        await Task.Delay(100);
        return new ChatResponse
        {
            Message = $"AI received: {request.Message}"
        };
    }
}