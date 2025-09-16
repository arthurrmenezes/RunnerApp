using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunnerApp.Application.Services.AccountContext.Interfaces;
using RunnerApp.Domain.ValueObjects;

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

    [HttpGet("{id}", Name = "GetAccountById")]
    [Authorize]
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
}
