using learnllm.Models;

namespace learnllm.Services;

public class DocumentRepository : IDocumentRepository
{
    public async Task AddAsync(Document document)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Document>> SearchAsync(float[] embeddings, int limit)
    {
        throw new NotImplementedException();
    }
}