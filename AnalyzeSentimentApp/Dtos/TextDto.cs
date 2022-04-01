namespace AnalyzeSentimentApp.Dtos;

public record TextDto
{
    public string content { get; set; }
    public int beginOffset { get; set; }
}