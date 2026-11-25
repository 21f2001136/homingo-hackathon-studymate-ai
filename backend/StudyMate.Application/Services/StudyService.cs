using StudyMate.Application.DTOs.Study;
using StudyMate.Application.Interfaces;

namespace StudyMate.Application.Services;

public class StudyService : IStudyService
{
    private readonly IAIService _aiService;

    public StudyService(IAIService aiService)
    {
        _aiService = aiService;
    }

    public async Task<ExplainResponseDto> ExplainTopicAsync(
        ExplainRequestDto request)
    {
        var explanation = await _aiService.GenerateExplanationAsync(
            request.Topic,
            request.Language,
            request.Level);

        return new ExplainResponseDto
        {
            Explanation = explanation,
            Summary = $"Summary of {request.Topic}",
            QuizQuestion = $"What is {request.Topic}?"
        };
    }
}