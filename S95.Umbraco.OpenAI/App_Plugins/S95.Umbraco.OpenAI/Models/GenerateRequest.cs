namespace S95.Umbraco.OpenAI.Models;

public class GenerateRequest
{
    public RequestType Type { get; set; }

    public string Text { get; set; } = string.Empty;

    public int MaximumLength { get; set; } = new();

    public BehaviorModel BehaviorModel { get; set; }
}