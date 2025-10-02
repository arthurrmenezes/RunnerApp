using RunnerApp.Domain.BoundedContexts.AccountContext.Entities;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Infrastructure.Data.Repositories.Interfaces;

public interface IAccountRepository
{
    public Task<Account?> GetAccountByIdAsync(IdValueObject accountId, CancellationToken cancellationToken);
}
