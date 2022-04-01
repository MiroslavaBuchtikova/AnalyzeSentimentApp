namespace AnalyzeSentimentApp.Dtos;

using System.Collections.Generic;

internal record MessageDto
{
    public List<string> messages { get; set; }
}