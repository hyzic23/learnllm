using learnllm.Models;

namespace learnllm.Services;

public class LlmService : ILlmService
{
    public async Task<ChatResponse> ChatAsync(ChatRequest request)
    {
        await Task.Delay(100);
        return new ChatResponse
        {
            Message = $"AI received: {request.Message}"
        };
    }
}