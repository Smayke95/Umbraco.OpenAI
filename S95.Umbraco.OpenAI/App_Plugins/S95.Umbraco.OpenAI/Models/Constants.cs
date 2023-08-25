namespace S95.Umbraco.OpenAI.Models;

public static class Constants
{
    public static readonly string ApiUrl = $"https://api.openai.com/v1/chat/completions";

    public static readonly string DefaultModel = "gpt-3.5-turbo";

    public static readonly string[] SystemMessage =
    {
        "Write content for website",
        "Generate a list of 20 keywords for best website SEO",
        "Generate meta title and meta description for best website SEO",
        "You will be provided with statements, and your task is to fix grammar for provided language.",
        "Convert provided text to another language. Both languages will be provided.",
    };
}