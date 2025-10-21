using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using RunnerApp.Application.Services.TrainingContext.Inputs;
using RunnerApp.Application.Services.TrainingContext.Interfaces;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Identity.Entities;
using RunnerApp.WebApi.Controllers.TrainingContext.Payloads;
using System.Security.Claims;

namespace RunnerApp.WebApi.Controllers.TrainingContext;

[ApiController]
[Route("api/v1/training")]
[EnableRateLimiting("fixed")]
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
        if (input.Location is null &&
            input.Distance is null &&
            input.Duration is null &&
            input.Date is null)
            throw new ArgumentException("At least one field must be provided for the update.");

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
    [Route("me/trainings")]
    [Authorize]
    public async Task<IActionResult> GetAllTrainingsForCurrentUserAsync(
        CancellationToken cancellationToken,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        if (pageNumber < 1) pageNumber = 1;

        if (pageSize < 1 || pageSize > 50)
            throw new ArgumentException("Page size must be between 1 and 50.");

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("Invalid token: user identifier is missing.");

        var applicationUser = await _userManager.FindByIdAsync(userId);
        if (applicationUser is null)
            throw new ArgumentException("User not found.");

        var trainings = await _trainingService.GetAllTrainingsForCurrentUserServiceAsync(
            accountId: IdValueObject.Factory(applicationUser.AccountId),
            pageNumber: pageNumber,
            pageSize: pageSize,
            cancellationToken: cancellationToken);

        return Ok(trainings);
    }
}
