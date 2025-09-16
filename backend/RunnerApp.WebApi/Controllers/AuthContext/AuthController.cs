using Microsoft.AspNetCore.Mvc;
using RunnerApp.Application.Services.AuthContext.Inputs;
using RunnerApp.Application.Services.AuthContext.Interfaces;
using RunnerApp.WebApi.Controllers.AuthContext.Payloads;

namespace RunnerApp.WebApi.Controllers.AuthContext;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUserAccountAsync(RegisterUserAccountPayload input, CancellationToken cancellationToken)
    {
        var serviceInput = RegisterUserAccountServiceInput.Factory(
            firstName: input.FirstName,
            surname: input.Surname,
            email: input.Email,
            password: input.Password,
            rePassword: input.RePassword);

        var result = await _authService.RegisterUserAccountServiceAsync(
            input: serviceInput, 
            cancellationToken: cancellationToken);

        return CreatedAtRoute("GetAccountById", new { id = result.AccountId }, result);
    }
}
