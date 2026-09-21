using learnllm.Models;

namespace learnllm.Services;

public interface IRagService
{
    Task<AskQuestionResponse> AskAsync(AskQuestionRequest request);
}