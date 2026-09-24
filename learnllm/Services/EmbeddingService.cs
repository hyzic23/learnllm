using learnllm.Config;
using Microsoft.Extensions.Options;
using OpenAI.Embeddings;

namespace learnllm.Services;

public class EmbeddingService : IEmbeddingService
{
    private readonly EmbeddingClient _embeddingClient;

    public EmbeddingService(IOptions<OpenAIOptions> options)
    {
        var settings = options.Value;
        _embeddingClient = new EmbeddingClient(
                                                settings.EmbeddingModel,
                                                settings.ApiKey);
    }

    public async Task<float[]> CreateEmbeddingAsync(string text)
    {
        var embedding = await _embeddingClient
                                                            .GenerateEmbeddingAsync(text);
        return embedding.Value
                        .ToFloats()
                        .ToArray();
    }
}