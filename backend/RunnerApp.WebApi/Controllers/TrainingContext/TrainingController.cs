using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunnerApp.Application.Services.TrainingContext.Inputs;
using RunnerApp.Application.Services.TrainingContext.Interfaces;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Identity;
using RunnerApp.WebApi.Controllers.TrainingContext.Payloads;

namespace RunnerApp.WebApi.Controllers.TrainingContext;

[ApiController]
[Route("api/v1/training")]
public class TrainingController : ControllerBase
{
    private readonly ITrainingService _trainingService;
    private readonly UserManager<ApplicationUser> _userManager;

    public TrainingController(ITrainingService trainingService, UserManager<ApplicationUser> userManager)
    {
        _trainingService = trainingService;
        _userManager = userManager;
	}

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateTrainingAsync(
        [FromBody] CreateTrainingPayload input,
        CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("Invalid token: 'sub' claim (user identifier) is missing or empty.");

        var applicationUser = await _userManager.FindByIdAsync(userId);
        if (applicationUser is null)
            throw new ArgumentException("The user associated with this token was not found.");

        var training = await _trainingService.CreateTrainingServiceAsync(
            input: CreateTrainingServiceInput.Factory(
                accountId: IdValueObject.Factory(applicationUser.AccountId),
                location: input.Location,
                distance: input.Distance,
                duration: input.Duration,
                date: input.Date),
            cancellationToken: cancellationToken);

        return CreatedAtAction("GetTrainingById", new { trainingId = training.TrainingId }, training);
    }

    [HttpGet]
    [Route("{trainingId}")]
    [Authorize]
    public async Task<IActionResult> GetTrainingByIdAsync(
        [FromRoute] string trainingId,
        CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("Invalid token: 'sub' claim (user identifier) is missing or empty.");

        var applicationUser = await _userManager.FindByIdAsync(userId);
        if (applicationUser is null)
            throw new ArgumentException("The user associated with this token was not found.");
        
        if (!Guid.TryParse(trainingId, out var guid))
            throw new ArgumentException("The provided ID is not a valid GUID.");

        var training = await _trainingService.GetTrainingByIdServiceAsync(
            trainingId: IdValueObject.Factory(guid),
            accountId: IdValueObject.Factory(applicationUser.AccountId),
            cancellationToken: cancellationToken);

        return Ok(training);
    }

    [HttpPatch]
    [Route("{trainingId}")]
    [Authorize]
    public async Task<IActionResult> UpdateTrainingByIdAsync(
        [FromRoute] string trainingId,
        [FromBody] UpdateTrainingByIdPayload input,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(trainingId, out var guid))
            throw new ArgumentException("The provided ID is not a valid GUID.");

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("Invalid token: user identifier is missing.");

        var applicationUser = await _userManager.FindByIdAsync(userId);
        if (applicationUser is null)
            throw new ArgumentException("User not found.");

        var training = await _trainingService.UpdateTrainingByIdServiceAsync(
            trainingId: IdValueObject.Factory(guid),
            accountId: applicationUser.AccountId,
            input: UpdateTrainingByIdServiceInput.Factory(
                location: input.Location,
                distance: input.Distance,
                duration: input.Duration,
                date: input.Date),
            cancellationToken: cancellationToken);

        return Ok(training);
    }

    [HttpDelete]
    [Route("{trainingId}")]
    [Authorize]
    public async Task<IActionResult> DeleteTrainingByIdAsync(
        [FromRoute] string trainingId,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(trainingId, out var guid))
            throw new ArgumentException("The provided ID is not a valid GUID.");

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("Invalid token: user identifier is missing.");

        var applicationUser = await _userManager.FindByIdAsync(userId);
        if (applicationUser is null)
            throw new ArgumentException("User not found.");

        await _trainingService.DeleteTrainingByIdServiceAsync(
            trainingId: IdValueObject.Factory(guid),
            accountId: applicationUser.AccountId,
            cancellationToken: cancellationToken);

        return NoContent();
    }

    [HttpGet]
    [Route("account/{accountId}/trainings")]
    [Authorize]
    public async Task<IActionResult> GetAllTrainingsByAccountIdAsync(
        [FromRoute] string accountId,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(accountId, out var guid))
            throw new ArgumentException("The provided ID is not a valid GUID.");

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("Invalid token: user identifier is missing.");

        var applicationUser = await _userManager.FindByIdAsync(userId);
        if (applicationUser is null)
            throw new ArgumentException("User not found.");

        var trainings = await _trainingService.GetAllTrainingsByAccountIdServiceAsync(
            accountId: IdValueObject.Factory(guid),
            callerAccountId: applicationUser.AccountId,
            cancellationToken: cancellationToken);
        return Ok(trainings);
    }
}
