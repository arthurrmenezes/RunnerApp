using RunnerApp.Application.Services.AccountContext.Inputs;
using RunnerApp.Application.Services.AccountContext.Outputs;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Application.Services.AccountContext.Interfaces;

public interface IAccountService
{
    public Task<GetAccountByIdServiceOutput> GetAccountByIdServiceAsync(
        IdValueObject accountId,
        IdValueObject callerAccountId,
        CancellationToken cancellationToken);

    public Task<UpdateAccountByIdServiceOutput> UpdateAccountByIdServiceAsync(
        IdValueObject accountId,
        IdValueObject callerAccountId,
        UpdateAccountByIdServiceInput input,
        CancellationToken cancellationToken);
}
