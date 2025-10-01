using RunnerApp.Application.Services.AccountContext.Inputs;
using RunnerApp.Application.Services.AccountContext.Interfaces;
using RunnerApp.Application.Services.AccountContext.Outputs;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Data.Repositories.Interfaces;

namespace RunnerApp.Application.Services.AccountContext;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<GetAccountByIdServiceOutput> GetAccountByIdServiceAsync(
        IdValueObject accountId,
        IdValueObject callerAccountId,
        CancellationToken cancellationToken)
    {
        if (accountId.ToString() != callerAccountId.ToString())
            throw new UnauthorizedAccessException("You are not allowed to view these account details.");

        var account = await _accountRepository.GetAccountByIdAsync(accountId, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException($"Account with ID {accountId} not found.");

        var output = GetAccountByIdServiceOutput.Factory(
            id: account.Id.ToString(),
            firstName: account.FirstName,
            surname: account.Surname,
            email: account.Email,
            createdAt: account.CreatedAt);

        return output;
    }

    public async Task<UpdateAccountByIdServiceOutput> UpdateAccountByIdServiceAsync(
        IdValueObject accountId,
        IdValueObject callerAccountId,
        UpdateAccountByIdServiceInput input, 
        CancellationToken cancellationToken)
    {
        if (input.FirstName is null &&
            input.Surname is null &&
            input.Email is null)
            throw new ArgumentException("At least one field must be provided for the update.");

        if (accountId.ToString() != callerAccountId.ToString())
            throw new UnauthorizedAccessException("You are not allowed to view these account details.");

        var account = await _accountRepository.GetAccountByIdAsync(accountId, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException($"Account with ID {accountId} was not found.");

        account.UpdateAccountDetails(
            firstName: input.FirstName,
            surname: input.Surname,
            email: input.Email);

        await _accountRepository.UpdateAccountByIdAsync(account, cancellationToken);

        var output = UpdateAccountByIdServiceOutput.Factory(
            id: account.Id.ToString(),
            firstName: account.FirstName,
            surname: account.Surname,
            email: account.Email,
            createdAt: account.CreatedAt);

        return output;
    }
}
