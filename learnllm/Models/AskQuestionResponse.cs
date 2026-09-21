namespace learnllm.Models;

public class AskQuestionResponse
{
    public string Answer { get; set; } = string.Empty;
    public List<string> Sources { get; set; } = [];
}

//Sample response
//  {
//      "answer": "Employees receive 15 vacation days every year.",
//      "sources": [
//          "Employee Handbook - Page 3"
//       ]
//  }