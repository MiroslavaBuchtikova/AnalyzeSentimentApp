namespace AnalyzeSentimentApp.Dtos;

using System.Collections.Generic;

public record SentimentResultDto
{
    public DocumentSentimentDto documentSentiment { get; set; }
    public string language { get; set; }
    public List<SentenceDto> sentences { get; set; }
}