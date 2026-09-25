using learnllm.Models;

namespace learnllm.Services;

/// <summary>
/// RagService Implementation
/// </summary>
public class RagService : IRagService
{
    private readonly IEmbeddingService _embeddingService;
    private readonly ILlmService  _llmService;
    private readonly IDocumentRepository  _documentRepository;

    public RagService(IEmbeddingService embeddingService, 
                      ILlmService llmService, 
                      IDocumentRepository documentRepository)
    {
        _embeddingService = embeddingService;
        _llmService = llmService;
        _documentRepository = documentRepository;
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
    /// <returns>AskQuestionResponse</returns>
    public async Task<AskQuestionResponse> AskAsync(AskQuestionRequest request)
    {
        // Step 1 - Create Embeddings : Convert question into embedding
        var questionEmbedding = await _embeddingService.CreateEmbeddingAsync(request.Question);
        
        // Step 2 - Search vector database for similar chunks: PostgreSQL
        var chunks = await _documentRepository.SearchAsync(questionEmbedding, 5);
        
        // Step 3 - Combine the retrieved chunks or Get relevant document chunks
        var context = string.Join("\n\n", chunks.Select(x => x.Content));
        
        // Step 4 - Build prompt
        var prompt = $"You are an employee HR assistant" +
                     $"Answer the user's question using only provided company documentation" +
                     $"If the answer cannot be found in the documentation, say you don't have enough information" +
                     $"Documentation: " +
                     $"{context}" +
                     $"Question:" +
                     $"{request.Question}";
        
        // Step 5 - Send prompt to LLM or Ask the LLM
        var response = await _llmService.ChatAsync(
            new ChatRequest{Message = prompt});
        
        //Step 6 - Return the answer
        return new AskQuestionResponse
        {
            Answer = response.Message,
            Sources = chunks.Select(x => $"{x.Source} - Page {x.PageNumber}").ToList()
        };
    }
}