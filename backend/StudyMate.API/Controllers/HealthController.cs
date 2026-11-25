using Microsoft.AspNetCore.Mvc;
using StudyMate.Application.Interfaces;

namespace StudyMate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly IHealthService _healthService;

    public HealthController(IHealthService healthService)
    {
        _healthService = healthService;
    }

    [HttpGet]
    public IActionResult GetHealth()
    {
        return Ok(new
        {
            status = "Healthy",
            message = _healthService.GetStatus()
        });
    }
}