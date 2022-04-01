namespace AnalyzeSentimentApp.Dtos;

internal record AnalyzeTextDto
{
    public string type { get; set; } = "PLAIN_TEXT";
    public string content { get; set; }
}