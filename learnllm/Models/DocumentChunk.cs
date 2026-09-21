namespace learnllm.Models;

public class DocumentChunk
{
    public int Id { get; set; }
    public int DocumentId { get; set; }
    public string Content { get; set; } = string.Empty;
    public float[] Embeddings { get; set; } = [];
    public int ChunkIndex { get; set; }
    public string? Source { get; set; }
    public string? PageNumber { get; set; }
}

//DocumentId: 15
// ChunkIndex: 7
// PageNumber: 37
// Source: Vacation Policy
// Content: "Employees receive 15 vacation days..."
// Embedding: [...]
// This metadata becomes very useful when we want to show users where an answer came from.