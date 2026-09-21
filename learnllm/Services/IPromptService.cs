namespace learnllm.Services;

public interface IPromptService
{
    Task<string> BuildLearningPrompt(string topic);
    Task<string> BuildCodeReviewPrompt(string code);
}