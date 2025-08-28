using Microsoft.AspNetCore.Mvc;
using RunnerApp.Application.Services.TrainingContext.Inputs;
using RunnerApp.Application.Services.TrainingContext.Interfaces;

namespace RunnerApp.WebApi.Controllers.TrainingContext;

[ApiController]
[Route("api/v1/training")]
public class TrainingController : ControllerBase
{
    private readonly ITrainingService _trainingService;

    public TrainingController(ITrainingService trainingService)
    {
        _trainingService = trainingService;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateTrainingAsync(
        [FromBody] CreateTrainingServiceInput input,
        CancellationToken cancellationToken)
    {
        return Ok();
    }
}
