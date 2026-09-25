using learnllm.Models;

namespace learnllm.Repository;

/// <summary>
/// This IDocumentRepository has three responsibilities
/// 1. Save a document
/// 2. Save each document chunk and it's embedding
///  3. Search for the most relevant chunks using vector similarity
/// </summary>
public interface IDocumentRepository
{
    /// <summary>
    /// Stores Documents
    /// </summary>
    /// <param name="document"></param>
    /// <returns>int</returns>
    Task<int> AddDocumentAsync(Document document);
    
    /// <summary>
    /// Add Chunks
    /// </summary>
    /// <param name="chunk"></param>
    /// <returns>int</returns>
    Task<int> AddChunkAsync(DocumentChunk chunk);
    
    /// <summary>
    /// Finds similar Documents
    /// </summary>
    /// <param name="embedding"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    Task<List<DocumentChunk>>  SearchAsync(float[] embedding, int limit);
}