using learnllm.Helper;

namespace learnllm.Services;

/// <summary>
/// DocumentProcessor Implementation will do the following below :
/// Step 1 - Receives the document
/// Step 2 - Chunk it
/// Step 3 - Generate Embeddings
/// Step 4 - Save the Chunks
/// </summary>
public class DocumentProcessor : IDocumentProcessor
{
    private readonly TextChunker _textChunker;
    private readonly IEmbeddingService _embeddingService;
    
    public DocumentProcessor(TextChunker textChunker, IEmbeddingService embeddingService)
    {
        _textChunker = textChunker;
        _embeddingService = embeddingService;
    }

    public async Task<int> ProcessAsync(string fileName, string text)
    {
        var chunks = _textChunker.ChunkText(text, chunkSize: 1000, overlap: 200);
        foreach (var chunk in chunks )
        {
            var embedding = await _embeddingService.CreateEmbeddingAsync(chunk);
            // Save chunk + embedding
        }
        return chunks.Count;
    }
}