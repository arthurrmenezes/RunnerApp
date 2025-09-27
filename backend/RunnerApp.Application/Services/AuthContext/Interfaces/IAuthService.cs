using RunnerApp.Application.Services.AuthContext.Inputs;
using RunnerApp.Application.Services.AuthContext.Outputs;

namespace RunnerApp.Application.Services.AuthContext.Interfaces;

public interface IAuthService
{
    public Task<RegisterUserAccountServiceOutput> RegisterUserAccountServiceAsync(
        RegisterUserAccountServiceInput input,
        CancellationToken cancellationToken);

    public Task<LoginUserAccountServiceOutput> LoginUserAccountServiceAsync(
        LoginUserAccountServiceInput input,
        CancellationToken cancellationToken);

    public Task<RefreshTokenServiceOutput> RefreshTokenServiceAsync(
        RefreshTokenServiceInput input,
        CancellationToken cancellationToken);
}
