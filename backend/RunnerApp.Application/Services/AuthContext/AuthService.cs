using Microsoft.AspNetCore.Identity;
using RunnerApp.Application.Services.AuthContext.Inputs;
using RunnerApp.Application.Services.AuthContext.Interfaces;
using RunnerApp.Application.Services.AuthContext.Outputs;
using RunnerApp.Domain.BoundedContexts.AccountContext.Entities;
using RunnerApp.Infrastructure.Identity;

namespace RunnerApp.Application.Services.AuthContext;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<RegisterUserAccountServiceOutput> RegisterUserAccountServiceAsync(
        RegisterUserAccountServiceInput input, 
        CancellationToken cancellationToken)
    {
        if (input.Password != input.RePassword)
            throw new ArgumentException("Passwords do not match.");

        var emailExists = await _userManager.FindByEmailAsync(input.Email);
        if (emailExists is not null)
            throw new ArgumentException($"An account with this email already exists.");

        var account = Account.Factory(
            firstName: input.FirstName,
            surname: input.Surname,
            email: input.Email);

        var user = new ApplicationUser
        {
            UserName = $"{input.FirstName} {input.Surname}",
            Email = input.Email,
            Account = account,
            AccountId = account.Id
        };

        var result = await _userManager.CreateAsync(user, input.Password);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        var output = RegisterUserAccountServiceOutput.Factory(
            accountId: account.Id.ToString(),
            firstName: account.FirstName.ToString(),
            surname: account.Surname.ToString(),
            email: account.Email.ToString(),
            createdAt: account.CreatedAt);

        return output;
    }
}
