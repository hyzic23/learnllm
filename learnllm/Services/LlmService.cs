using System.Diagnostics.CodeAnalysis;
using learnllm.Config;
using learnllm.Models;
using Microsoft.Extensions.Options;
using OpenAI.Responses;

namespace learnllm.Services;

[Experimental("OPENAI001")]
public class LlmService : ILlmService
{
    private readonly LlmOptions _llmOptions;
    private readonly ResponsesClient _responsesClient;
    private readonly string _model;

    public LlmService(IOptions<OpenAIOptions> options)
    {
        var settings = options.Value;
        _model = settings.ChatModel;
        _responsesClient = new ResponsesClient(
            settings.ApiKey);
    }

    public async Task<ChatResponse> ChatAsync(ChatRequest request)
    {
        var response = await _responsesClient.CreateResponseAsync(
                              _model, request.Message  );
                                
        return new ChatResponse
        {
            Message = response.Value.GetOutputText()
            
        };
    }
}