using learnllm.Helper;
using learnllm.Models;

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
    private readonly IDocumentRepository  _documentRepository;
    
    public DocumentProcessor(TextChunker textChunker, 
                             IEmbeddingService embeddingService, 
                             IDocumentRepository documentRepository)
    {
        _textChunker = textChunker;
        _embeddingService = embeddingService;
        _documentRepository = documentRepository;
    }

    public async Task<int> ProcessAsync(string fileName, string text)
    {
        var chunks = _textChunker.ChunkText(text, chunkSize: 1000, overlap: 200);
        var index = 0;
        foreach (var chunk in chunks)
        {
            var embedding = await _embeddingService.CreateEmbeddingAsync(chunk);
            // Save chunk + embedding
            var documentChunk = new DocumentChunk
            {
                Content = chunk,
                Embeddings = embedding,
                ChunkIndex = index,
                Source = fileName   //employee-handbook.pdf
            };
            await _documentRepository.AddChunkAsync(documentChunk);
            index++;
        }
        return chunks.Count;
    }
}