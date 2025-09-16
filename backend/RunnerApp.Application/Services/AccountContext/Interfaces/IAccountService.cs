using RunnerApp.Application.Services.AccountContext.Outputs;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Application.Services.AccountContext.Interfaces;

public interface IAccountService
{
    public Task<GetAccountByIdServiceOutput> GetAccountByIdServiceAsync(
        IdValueObject accountId, 
        CancellationToken cancellationToken);
}
