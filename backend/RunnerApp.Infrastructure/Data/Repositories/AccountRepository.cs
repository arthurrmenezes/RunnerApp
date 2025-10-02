using Microsoft.EntityFrameworkCore;
using RunnerApp.Domain.BoundedContexts.AccountContext.Entities;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Data.Repositories.Interfaces;

namespace RunnerApp.Infrastructure.Data.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly DataContext _dataContext;

    public AccountRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Account?> GetAccountByIdAsync(IdValueObject accountId, CancellationToken cancellationToken)
    {
        return await _dataContext.Accounts.FirstOrDefaultAsync(a => a.Id.Equals(accountId), cancellationToken);
    }
}
