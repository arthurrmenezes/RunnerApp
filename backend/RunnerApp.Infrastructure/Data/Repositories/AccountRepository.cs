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

    public async Task CreateAccountAsync(Account account, CancellationToken cancellationToken)
    {
        await _dataContext.Accounts.AddAsync(account, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsEmailExistsAsync(EmailValueObject email, CancellationToken cancellationToken)
    {
        return await _dataContext.Accounts.AnyAsync(a => a.Email.Equals(email), cancellationToken);
    }

    public async Task<Account?> GetAccountById(IdValueObject id, CancellationToken cancellationToken)
    {
        return await _dataContext.Accounts.FirstOrDefaultAsync(a => a.Id.Equals(id), cancellationToken);
    }
}
