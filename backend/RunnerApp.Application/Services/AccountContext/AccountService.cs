using RunnerApp.Application.Services.AccountContext.Inputs;
using RunnerApp.Application.Services.AccountContext.Interfaces;
using RunnerApp.Application.Services.AccountContext.Outputs;
using RunnerApp.Domain.BoundedContexts.AccountContext.Entities;
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

    public async Task<CreateAccountServiceOutput> CreateAccountServiceAsync(
        CreateAccountServiceInput input, 
        CancellationToken cancellationToken)
    {
        var emailExists = await _accountRepository.IsEmailExistsAsync(input.Email, cancellationToken);
        if (emailExists)
            throw new ArgumentException("An account with this email already exists.", nameof(input.Email));

        var account = Account.Factory(
            firstName: input.FirstName,
            surname: input.Surname,
            email: input.Email);

        await _accountRepository.CreateAccountAsync(account, cancellationToken);

        return CreateAccountServiceOutput.Factory(
            id: account.Id.ToString(),
            firstName: account.FirstName,
            surname: account.Surname,
            email: account.Email,
            createdAt: account.CreatedAt,
            trainings: account.Trainings);
    }

    public async Task<GetAccountByIdServiceOutput> GetAccountByIdServiceAsync(IdValueObject id, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountById(id, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException($"Account with ID {id} not found.");

        return GetAccountByIdServiceOutput.Factory(
            id: account.Id.ToString(),
            firstName: account.FirstName,
            surname: account.Surname,
            email: account.Email,
            createdAt: account.CreatedAt);
    }
}
