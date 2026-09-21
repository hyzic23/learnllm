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

    public async Task<string> BuildCodeReviewPrompt(string code)
    {
        return $"You are a Senior .NET code reviewer." +
               $"Review the following C# code " +
               $"Look for :" +
               $" 1.  Bugs" +
               $" 2.  Security problems" +
               $" 3.  Performance problems" +
               $" 4.  Readability issues " +
               $" 5.  .NET best practice violations " +
               $"For each issue :" +
               $"- Explain the problem" +
               $"- Explain why it matters" +
               $"- Suggest an improvement" +
               $" Code :" +
               $"{code}";
    }
}