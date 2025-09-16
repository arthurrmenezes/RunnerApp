using RunnerApp.Application.Services.AuthContext.Inputs;
using RunnerApp.Application.Services.AuthContext.Outputs;

namespace RunnerApp.Application.Services.AuthContext.Interfaces;

public interface IAuthService
{
    public Task<RegisterUserAccountServiceOutput> RegisterUserAccountServiceAsync(
        RegisterUserAccountServiceInput input,
        CancellationToken cancellationToken);
}
