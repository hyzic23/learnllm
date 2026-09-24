namespace learnllm.Services;

public interface IDocumentTextExtractor
{
    Task<string> ExtractTextAsync(Stream document);
}