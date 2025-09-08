using Microsoft.AspNetCore.Mvc;
using RunnerApp.Application.Services.TrainingContext.Inputs;
using RunnerApp.Application.Services.TrainingContext.Interfaces;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.WebApi.Controllers.TrainingContext.Payloads;

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
    public async Task<IActionResult> CreateTrainingAsync(
        [FromBody] CreateTrainingPayload input,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(input.AccountId, out var guid))
            throw new ArgumentException("The provided ID is not a valid GUID.");

        var training = await _trainingService.CreateTrainingServiceAsync(
            input: CreateTrainingServiceInput.Factory(
                accountId: IdValueObject.Factory(guid),
                location: input.Location,
                distance: input.Distance,
                duration: input.Duration,
                date: input.Date),
            cancellationToken: cancellationToken);

        return CreatedAtAction("GetTrainingById", new { id = training.Id }, training);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetTrainingByIdAsync(
        string id,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var guid))
            throw new ArgumentException("The provided ID is not a valid GUID.");

        var training = await _trainingService.GetTrainingByIdServiceAsync(
            id: IdValueObject.Factory(guid),
            cancellationToken: cancellationToken);

        return Ok(training);
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<IActionResult> UpdateTrainingByIdAsync(
        string id,
        [FromBody] UpdateTrainingByIdServiceInput input,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var guid))
            throw new ArgumentException("The provided ID is not a valid GUID.");

        if (input.Location is null && input.Distance is null && input.Duration is null && input.Date is null)
            return BadRequest("At least one field must be provided for the update.");

        var training = await _trainingService.UpdateTrainingByIdServiceAsync(
            id: IdValueObject.Factory(guid),
            input: input,
            cancellationToken: cancellationToken);

        return Ok(training);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteTrainingByIdAsync(
        string id,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var guid))
            throw new ArgumentException("The provided ID is not a valid GUID.");

        await _trainingService.DeleteTrainingByIdServiceAsync(
            id: IdValueObject.Factory(guid),
            cancellationToken: cancellationToken);

        return NoContent();
    }
}
