using Microsoft.AspNetCore.Mvc;
using RunnerApp.Application.Services.AccountContext.Inputs;
using RunnerApp.Application.Services.AccountContext.Interfaces;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.WebApi.Controllers.AccountContext.Payloads;

namespace RunnerApp.WebApi.Controllers.AccountContext;

[ApiController]
[Route("api/v1/account")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccountAsync(
        [FromBody] CreateAccountPayload input,
        CancellationToken cancellationToken)
    {
        var account = await _accountService.CreateAccountServiceAsync(
            input: CreateAccountServiceInput.Factory(
                firstName: input.FirstName,
                surname: input.Surname,
                email: input.Email),
            cancellationToken: cancellationToken);

        return CreatedAtAction("GetAccountById", new { id = account.Id }, account);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAccountByIdAsync(
        string id, 
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var guid))
            throw new ArgumentException("The provided ID is not a valid GUID.");

        var account = await _accountService.GetAccountByIdServiceAsync(
            accountId: IdValueObject.Factory(guid), 
            cancellationToken: cancellationToken);
        return Ok(account);
    }

    [HttpGet]
    [Route("{id}/trainings")]
    public async Task<IActionResult> GetAllTrainingsByAccountIdAsync(
        string id, 
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var guid))
            throw new ArgumentException("The provided ID is not a valid GUID.");

        var trainings = await _accountService.GetAllTrainingsByAccountIdServiceAsync(
            accountId: IdValueObject.Factory(guid), 
            cancellationToken: cancellationToken);
        return Ok(trainings);
    }
}
