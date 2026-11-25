using Microsoft.AspNetCore.Mvc;
using StudyMate.Application.DTOs.Study;
using StudyMate.Application.Interfaces;

namespace StudyMate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudyController : ControllerBase
{
    private readonly IStudyService _studyService;

    public StudyController(IStudyService studyService)
    {
        _studyService = studyService;
    }

    [HttpPost("explain")]
    public async Task<IActionResult> ExplainTopic(
        ExplainRequestDto request)
    {
        var response =
            await _studyService.ExplainTopicAsync(request);

        return Ok(response);
    }
}