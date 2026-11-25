using System.Text.Json.Serialization;

namespace StudyMate.Infrastructure.AI.Models.Responses;

public class GeminiResponse
{
    [JsonPropertyName("candidates")]
    public List<GeminiCandidate>? Candidates { get; set; }
}

public class GeminiCandidate
{
    [JsonPropertyName("content")]
    public GeminiResponseContent? Content { get; set; }
}

public class GeminiResponseContent
{
    [JsonPropertyName("parts")]
    public List<GeminiResponsePart>? Parts { get; set; }
}

public class GeminiResponsePart
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }
}