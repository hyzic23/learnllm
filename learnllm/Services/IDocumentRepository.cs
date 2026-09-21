using learnllm.Models;

namespace learnllm.Services;

public interface IDocumentRepository
{
    /// <summary>
    /// Stores Documents
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    Task AddAsync(Document document);
    
    /// <summary>
    /// Finds similar Documents
    /// </summary>
    /// <param name="embeddings"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    Task<List<Document>>  SearchAsync(float[] embeddings, int limit);
}