namespace learnllm.Models;

public class Document
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public List<DocumentChunk> Chunks { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}