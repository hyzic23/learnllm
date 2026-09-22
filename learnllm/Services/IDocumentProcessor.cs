namespace learnllm.Services;

/// <summary>
/// IDocumentProcessor will do the following below :
/// Step 1 - Receives the document
/// Step 2 - Chunk it
/// Step 3 - Generate Embeddings
/// Step 4 - Save the Chunks
/// </summary>
public interface IDocumentProcessor
{
    Task<int> ProcessAsync(string fileName, string text);
}