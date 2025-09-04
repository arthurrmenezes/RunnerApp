using RunnerApp.Application.Services.AccountContext.Inputs;
using RunnerApp.Application.Services.AccountContext.Outputs;

namespace RunnerApp.Application.Services.AccountContext.Interfaces;

public interface IAccountService
{
    public Task<CreateAccountServiceOutput> CreateAccountServiceAsync(
        CreateAccountServiceInput input, 
        CancellationToken cancellationToken);
}
