namespace AnalyzeSentimentApp.Dtos;

public record DocumentSentimentDto
{
    public decimal magnitude { get; set; }
    public decimal score { get; set; }
}