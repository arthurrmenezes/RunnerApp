using RunnerApp.Application.Services.AccountContext.Inputs;
using RunnerApp.Application.Services.AccountContext.Interfaces;
using RunnerApp.Application.Services.AccountContext.Outputs;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Data.Repositories.Interfaces;
using RunnerApp.Infrastructure.Data.UnitOfWork.Interfaces;

namespace RunnerApp.Application.Services.AccountContext;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetUserAccountDetailsServiceOutput> GetUserAccountDetailsServiceAsync(
        IdValueObject accountId,
        CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountByIdAsync(accountId, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException($"Account with ID {accountId} not found.");

        var output = GetUserAccountDetailsServiceOutput.Factory(
            id: account.Id.ToString(),
            firstName: account.FirstName,
            surname: account.Surname,
            email: account.Email,
            createdAt: account.CreatedAt);

        return output;
    }

    public async Task<UpdateAccountServiceOutput> UpdateAccountServiceAsync(
        IdValueObject accountId,
        UpdateAccountServiceInput input, 
        CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountByIdAsync(accountId, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException($"Account with ID {accountId} was not found.");

        account.UpdateAccountDetails(
            firstName: input.FirstName,
            surname: input.Surname,
            email: input.Email);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var output = UpdateAccountServiceOutput.Factory(
            id: account.Id.ToString(),
            firstName: account.FirstName,
            surname: account.Surname,
            email: account.Email,
            createdAt: account.CreatedAt);

        return output;
    }
}
