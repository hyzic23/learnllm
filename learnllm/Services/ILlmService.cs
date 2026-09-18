using learnllm.Models;

namespace learnllm.Services;

public interface ILlmService
{
    Task<ChatResponse> ChatAsync(ChatRequest request);
}