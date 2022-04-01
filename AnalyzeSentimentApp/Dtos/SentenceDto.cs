namespace AnalyzeSentimentApp.Dtos;

public record SentenceDto
{
    public TextDto text { get; set; }

    public DocumentSentimentDto sentiment { get; set; }
}