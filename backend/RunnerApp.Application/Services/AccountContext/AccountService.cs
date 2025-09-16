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
