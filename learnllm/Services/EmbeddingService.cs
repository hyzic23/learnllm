namespace learnllm.Services;

public class EmbeddingService : IEmbeddingService
{
    private readonly float[] _fakeEmbeddings = [0.12f, -0.45f, 0.88f, 0.31f];
    public async Task<float[]> CreateEmbeddingAsync(string text)
    {
        await Task.Delay(100);
        return _fakeEmbeddings;
    }
}