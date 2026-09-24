using learnllm.Models;
using Npgsql;
using Pgvector;

namespace learnllm.Services;

/// <summary>
/// This DocumentRepository has three responsibilities
/// 1. Save a document
/// 2. Save each document chunk and it's embedding
///  3. Search for the most relevant chunks using vector similarity
/// </summary>
public class DocumentRepository : IDocumentRepository
{
    private readonly string _connectionString;
    private readonly NpgsqlDataSource _dataSource;

    public DocumentRepository(string connectionString, NpgsqlDataSource dataSource)
    {
        _connectionString = connectionString;
        _dataSource = dataSource;
    }

    public async Task<int> AddDocumentAsync(Document document)
    {
        const string query = "" +
                             "INSERT INTO document (file_name, created_at) " +
                             "VALUES (@name, @created_at)" +
                             "Returning id;" +
                             "";
        await using var connection = await _dataSource.OpenConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("fileName", document.FileName);
        command.Parameters.AddWithValue("createdAt", document.CreatedAt);
        var result = await command.ExecuteScalarAsync();
        return Convert.ToInt32(result);
    }

    public async Task<int> AddChunkAsync(DocumentChunk chunk)
    {
        const string sql = """
                           INSERT INTO document_chunks
                           (
                               document_id,
                               content,
                               embedding,
                               chunk_index,
                               source,
                               page_number
                           )
                           VALUES
                           (
                               @documentId,
                               @content,
                               @embedding,
                               @chunkIndex,
                               @source,
                               @pageNumber
                           )
                           RETURNING id;
                           """;
        await using var connection = await _dataSource.OpenConnectionAsync();
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue(
            "documentId",
            chunk.DocumentId);

        command.Parameters.AddWithValue(
            "content",
            chunk.Content);

        command.Parameters.AddWithValue(
            "embedding",
            new Vector(chunk.Embeddings));

        command.Parameters.AddWithValue(
            "chunkIndex",
            chunk.ChunkIndex);

        command.Parameters.AddWithValue(
            "source",
            (object?)chunk.Source ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "pageNumber",
            (object?)chunk.PageNumber ?? DBNull.Value);

        var result =
            await command.ExecuteScalarAsync();

        return Convert.ToInt32(result);
    }

    public async Task<List<DocumentChunk>> SearchAsync(float[] embedding, int limit)
    {
        var results = new List<DocumentChunk>();
        const string sql = """
                           SELECT
                               id,
                               document_id,
                               content,
                               embedding,
                               chunk_index,
                               source,
                               page_number
                           FROM document_chunks
                           ORDER BY embedding <=> @embedding
                           LIMIT @limit;
                           """;
        var chunks = new List<DocumentChunk>();

        await using var connection = await _dataSource.OpenConnectionAsync();
        await connection.OpenAsync();
        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue(
            "embedding",
            new Vector(embedding));

        command.Parameters.AddWithValue(
            "limit",
            limit);

        await using var reader =
            await command.ExecuteReaderAsync();
        
        while (await reader.ReadAsync())
        {
            var chunk = new DocumentChunk
            {
                Id = reader.GetInt32(
                    reader.GetOrdinal("id")),

                DocumentId = reader.GetInt32(
                    reader.GetOrdinal("document_id")),

                Content = reader.GetString(
                    reader.GetOrdinal("content")),

                ChunkIndex = reader.GetInt32(
                    reader.GetOrdinal("chunk_index")),

                Source = reader.IsDBNull(
                    reader.GetOrdinal("source"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("source")),

                PageNumber = reader.IsDBNull(
                    reader.GetOrdinal("page_number"))
                    ? null
                    : reader.GetInt32(
                        reader.GetOrdinal("page_number"))
            };
            chunks.Add(chunk);
        }
        return chunks;
    }
}