using Microsoft.AspNetCore.Mvc;
using RunnerApp.Application.Services.AccountContext.Inputs;
using RunnerApp.Application.Services.AccountContext.Interfaces;
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

        return Ok(account);
        //return CreatedAtAction(nameof(GetAccountByIdAsync), new { id = account.Id }, account);
    }
}
