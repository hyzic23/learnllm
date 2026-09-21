namespace learnllm.Services;

public class PromptService : IPromptService
{
    public async Task<string> BuildLearningPrompt(string topic)
    {
        return $"You are a Senior .NET Developer teaching a beginner." +
               $"The developer understands C# and .NET but is new to AI and LLMs" +
               $"Explain the following topic:" +
               $"{topic}" +
               $" Requirement:" +
               $"- Use simple language" +
               $"- Explain the basic concept first" +
               $"- Give a real world analogy" +
               $"- Provide a practical C# example" +
               $"- Example the code" +
               $"- Mention common mistakes.";
    }
}