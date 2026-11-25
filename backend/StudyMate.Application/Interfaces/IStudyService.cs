using StudyMate.Application.DTOs.Study;

namespace StudyMate.Application.Interfaces;

public interface IStudyService
{
    Task<ExplainResponseDto> ExplainTopicAsync(ExplainRequestDto request);
}