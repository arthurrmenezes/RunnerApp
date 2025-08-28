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
        var training = await _trainingService.CreateTrainingServiceAsync(
            input: CreateTrainingServiceInput.Factory(
                location: input.Location, 
                distance: input.Distance, 
                duration: input.Duration, 
                date: input.Date),
            cancellationToken: cancellationToken);

        return StatusCode(201, training);
    }
}
