namespace learnllm.Models;

public class AskQuestionRequest
{
    public string Question { get; set; } = string.Empty;
}

// Sample json request
// {
//      "question": "How many vacation days do employees receive?"
// }