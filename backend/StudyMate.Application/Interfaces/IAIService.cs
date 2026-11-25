namespace StudyMate.Application.Interfaces;

public interface IAIService
{
    Task<string> GenerateExplanationAsync(
        string topic,
        string language,
        string level);
}