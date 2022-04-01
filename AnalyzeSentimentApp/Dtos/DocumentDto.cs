namespace AnalyzeSentimentApp.Dtos;

internal record DocumentDto
{
    public AnalyzeTextDto document { get; set; }
}