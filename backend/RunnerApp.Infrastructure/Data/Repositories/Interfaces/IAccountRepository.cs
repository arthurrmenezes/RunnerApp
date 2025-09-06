using RunnerApp.Domain.BoundedContexts.AccountContext.Entities;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Infrastructure.Data.Repositories.Interfaces;

public interface IAccountRepository
{
    public Task CreateAccountAsync(Account account, CancellationToken cancellationToken);

    public Task<bool> IsEmailExistsAsync(EmailValueObject email, CancellationToken cancellationToken);

    public Task<Account?> GetAccountById(IdValueObject id, CancellationToken cancellationToken);
}
