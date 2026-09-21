using learnllm.Models;

namespace learnllm.Services;

/// <summary>
/// RagService Implementation
/// </summary>
public class RagService : IRagService
{
    private readonly IEmbeddingService _embeddingService;
    private readonly ILlmService  _llmService;

    public RagService(IEmbeddingService embeddingService, ILlmService llmService)
    {
        _embeddingService = embeddingService;
        _llmService = llmService;
    }

    /// <summary>
    /// This following happens in this method for the AskAsync()
    /// 1. Create embeddings
    /// 2. Search Vector Database
    /// 3. Get Relevant Chunks
    /// 4. Build Prompt
    /// 5. Send Prompt to LLM
    /// 6. Return Answer
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<AskQuestionResponse> AskAsync(AskQuestionRequest request)
    {
        // Step 1
        var questionEmbedding = _embeddingService.CreateEmbeddingAsync(request.Question);
        
        // Step 2
        // Search vector database
        
        // Step 3
        // Get relevant document chunks
        
        // Step 4
        // Build prompt
        
        // Step 5
        // Send prompt to LLM

        return new AskQuestionResponse
        {
            Answer = "Employee receive 15 vacation days every year",
            Sources = 
            [
                "Employee Handbook"
            ]
        };
    }
}