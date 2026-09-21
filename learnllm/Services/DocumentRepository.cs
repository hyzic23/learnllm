using learnllm.Models;
using Npgsql;
using Pgvector;

namespace learnllm.Services;

public class DocumentRepository : IDocumentRepository
{
    private readonly NpgsqlDataSource _dataSource;
    public async Task AddAsync(Document document)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DocumentChunk>> SearchAsync(float[] embedding, int limit)
    {
        var results = new List<DocumentChunk>();
        await using var command = _dataSource.CreateCommand("""
                                                            SELECT
                                                                id,
                                                                document_id,
                                                                content,
                                                                chunk_index,
                                                                source,
                                                                page_number
                                                            FROM document_chunks
                                                            ORDER BY embedding <=> $1
                                                            LIMIT $2
                                                            """);
        command.Parameters.AddWithValue(new Vector(embedding));
        command.Parameters.AddWithValue(limit);
        await using var reader = await command.ExecuteReaderAsync();
        
        while (await reader.ReadAsync())
        {
            results.Add(new DocumentChunk
            {
                Id = reader.GetInt32(0),
                DocumentId = reader.GetInt32(1),
                Content = reader.GetString(2),
                ChunkIndex = reader.GetInt32(3),
                Source = reader.IsDBNull(4)
                    ? null
                    : reader.GetString(4),
                PageNumber = reader.IsDBNull(5)
                    ? null
                    : reader.GetInt32(5)
            });
        }

        return results;
    }
}