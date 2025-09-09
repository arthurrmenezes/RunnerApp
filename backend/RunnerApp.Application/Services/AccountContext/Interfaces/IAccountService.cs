using RunnerApp.Application.Services.AccountContext.Inputs;
using RunnerApp.Application.Services.AccountContext.Outputs;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Application.Services.AccountContext.Interfaces;

public interface IAccountService
{
    public Task<CreateAccountServiceOutput> CreateAccountServiceAsync(
        CreateAccountServiceInput input, 
        CancellationToken cancellationToken);

    public Task<GetAccountByIdServiceOutput> GetAccountByIdServiceAsync(
        IdValueObject accountId, 
        CancellationToken cancellationToken);

    public Task<GetAllTrainingsByAccountIdServiceOutput> GetAllTrainingsByAccountIdServiceAsync(
        IdValueObject accountId, 
        CancellationToken cancellationToken);
}
