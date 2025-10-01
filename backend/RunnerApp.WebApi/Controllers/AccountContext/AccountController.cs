using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunnerApp.Application.Services.AccountContext.Inputs;
using RunnerApp.Application.Services.AccountContext.Interfaces;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Identity.Entities;
using RunnerApp.WebApi.Controllers.AccountContext.Payloads;

namespace RunnerApp.WebApi.Controllers.AccountContext;

[ApiController]
[Route("api/v1/account")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(IAccountService accountService, UserManager<ApplicationUser> userManager)
    {
        _accountService = accountService;
        _userManager = userManager;
    }

    [HttpGet("{accountId}", Name = "GetAccountById")]
    [Authorize]
    public async Task<IActionResult> GetAccountByIdAsync(
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

        var account = await _accountService.GetAccountByIdServiceAsync(
            accountId: IdValueObject.Factory(guid),
            callerAccountId: applicationUser.AccountId,
            cancellationToken: cancellationToken);
        return Ok(account);
    }

    [HttpPatch]
    [Route("{accountId}")]
    [Authorize]
    public async Task<IActionResult> UpdateAccountAsync(
        [FromRoute] string accountId,
        [FromBody] UpdateAccountPayload input,
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

        var account = await _accountService.UpdateAccountByIdServiceAsync(
            accountId: IdValueObject.Factory(guid),
            callerAccountId: applicationUser.AccountId,
            input: UpdateAccountByIdServiceInput.Factory(
                firstName: input.FirstName,
                surname: input.Surname,
                email: input.Email),
            cancellationToken: cancellationToken);

        return Ok(account);
    }
}
