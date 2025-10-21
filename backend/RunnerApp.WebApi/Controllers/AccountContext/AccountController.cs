﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using RunnerApp.Application.Services.AccountContext.Inputs;
using RunnerApp.Application.Services.AccountContext.Interfaces;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Identity.Entities;
using RunnerApp.WebApi.Controllers.AccountContext.Payloads;

namespace RunnerApp.WebApi.Controllers.AccountContext;

[ApiController]
[Route("api/v1/account")]
[EnableRateLimiting("fixed")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(IAccountService accountService, UserManager<ApplicationUser> userManager)
    {
        _accountService = accountService;
        _userManager = userManager;
    }

    [HttpGet("me", Name = "GetUserAccountDetails")]
    [Authorize]
    public async Task<IActionResult> GetUserAccountDetailsAsync(CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("User identifier could not be found in the token.");

        var applicationUser = await _userManager.FindByIdAsync(userId);
        if (applicationUser is null)
            throw new ArgumentException("User not found.");

        var account = await _accountService.GetUserAccountDetailsServiceAsync(
            accountId: IdValueObject.Factory(applicationUser.AccountId),
            cancellationToken: cancellationToken);

        return Ok(account);
    }

    [HttpPatch]
    [Route("me")]
    [Authorize]
    public async Task<IActionResult> UpdateAccountAsync(
        [FromBody] UpdateAccountPayload input,
        CancellationToken cancellationToken)
    {
        if (input.FirstName is null &&
            input.Surname is null &&
            input.Email is null)
            throw new ArgumentException("At least one field must be provided for the update.");

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("Invalid token: user identifier is missing.");

        var applicationUser = await _userManager.FindByIdAsync(userId);
        if (applicationUser is null)
            throw new ArgumentException("User not found.");

        var account = await _accountService.UpdateAccountServiceAsync(
            accountId: IdValueObject.Factory(applicationUser.AccountId),
            input: UpdateAccountServiceInput.Factory(
                firstName: input.FirstName,
                surname: input.Surname,
                email: input.Email),
            cancellationToken: cancellationToken);

        return Ok(account);
    }
}
