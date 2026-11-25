using StudyMate.Application.Interfaces;

namespace StudyMate.Application.Services;

public class HealthService : IHealthService
{
    public string GetStatus()
    {
        return "StudyMate API is runninng!";
    }
}