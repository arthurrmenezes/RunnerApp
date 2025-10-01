using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using RunnerApp.Application.Services.AuthContext.Inputs;
using RunnerApp.Application.Services.AuthContext.Interfaces;
using RunnerApp.WebApi.Controllers.AuthContext.Payloads;

namespace RunnerApp.WebApi.Controllers.AuthContext;

[ApiController]
[Route("api/v1/auth")]
[EnableRateLimiting("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
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

        return CreatedAtRoute("GetAccountById", new { accountId = result.AccountId }, result);
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginUserAccountAsync(LoginUserAccountPayload input, CancellationToken cancellationToken)
    {
        var serviceInput = LoginUserAccountServiceInput.Factory(
            email: input.Email,
            password: input.Password);
        
        var result = await _authService.LoginUserAccountServiceAsync(
            input: serviceInput, 
            cancellationToken: cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Route("refresh-token")]
    [Authorize]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenPayload input, CancellationToken cancellationToken)
    {
        var serviceInput = RefreshTokenServiceInput.Factory(
            refreshToken: input.RefreshToken);
        var result = await _authService.RefreshTokenServiceAsync(serviceInput, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Route("logout")]
    [Authorize]
    public async Task<IActionResult> LogoutAsync(CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        await _authService.LogoutServiceAsync(
            userId: userId,
            cancellationToken: cancellationToken);
        return NoContent();
    }

    [HttpPost]
    [Route("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordPayload input, CancellationToken cancellationToken)
    {
        if (input.NewPassword != input.ConfirmNewPassword)
            throw new ArgumentException("The new password and confirmation password do not match.");

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User identifier could not be found in the token.");

        var serviceInput = ChangePasswordInput.Factory(
            userId: userId,
            currentPassword: input.CurrentPassword,
            newPassword: input.NewPassword);

        var result = await _authService.ChangePasswordAsync(serviceInput, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return NoContent();
    }
}
