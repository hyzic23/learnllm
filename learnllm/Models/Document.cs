namespace learnllm.Models;

public class Document
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public float[] Embeddings { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}