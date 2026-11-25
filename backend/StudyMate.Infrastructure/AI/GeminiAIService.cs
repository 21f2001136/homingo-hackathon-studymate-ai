using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using StudyMate.Application.Interfaces;
using StudyMate.Infrastructure.AI.Models.Requests;
using StudyMate.Infrastructure.AI.Models.Responses;
using StudyMate.Infrastructure.Configuration;

namespace StudyMate.Infrastructure.AI;

public class GeminiAIService : IAIService
{
    private readonly HttpClient _httpClient;
    private readonly GeminiOptions _options;

    public GeminiAIService(
        HttpClient httpClient,
        IOptions<GeminiOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<string> GenerateExplanationAsync(
        string topic,
        string language,
        string level)
    {
        var prompt = $"""
Explain the topic "{topic}".

Language: {language}
Level: {level}

Keep the explanation simple and beginner friendly.
Use headings and bullet points where appropriate.
""";

        var request = new GeminiRequest
        {
            Contents =
            [
                new GeminiRequestContent
                {
                    Parts =
                    [
                        new GeminiRequestPart
                        {
                            Text = prompt
                        }
                    ]
                }
            ]
        };

        var url =
            $"https://generativelanguage.googleapis.com/v1beta/models/{_options.Model}:generateContent?key={_options.ApiKey}";

        HttpResponseMessage? response = null;

        // Retry up to 3 times if Gemini is temporarily overloaded
        for (int attempt = 1; attempt <= 3; attempt++)
        {
            response = await _httpClient.PostAsJsonAsync(url, request);

            if (response.IsSuccessStatusCode)
            {
                break;
            }

            if ((int)response.StatusCode == 503)
            {
                Console.WriteLine($"Gemini busy. Retry {attempt}/3...");
                await Task.Delay(2000);
                continue;
            }

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Gemini API Error:\n{error}");
        }

        if (response == null || !response.IsSuccessStatusCode)
        {
            throw new Exception(
                "Gemini API is temporarily unavailable. Please try again in a few seconds.");
        }

        var geminiResponse =
            await response.Content.ReadFromJsonAsync<GeminiResponse>();

        var text = geminiResponse?
            .Candidates?
            .FirstOrDefault()?
            .Content?
            .Parts?
            .FirstOrDefault()?
            .Text;

        if (string.IsNullOrWhiteSpace(text))
        {
            return "Gemini returned an empty response.";
        }

        return text;
    }
}