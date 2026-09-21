namespace learnllm.Dtos;

public class LlmDto
{
    public record EmbeddingResponse(string Text, float[] Embeddings);
}