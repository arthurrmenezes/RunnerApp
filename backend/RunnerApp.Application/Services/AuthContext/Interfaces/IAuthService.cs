using Microsoft.AspNetCore.Identity;
using RunnerApp.Application.Services.AuthContext.Inputs;
using RunnerApp.Application.Services.AuthContext.Outputs;

namespace RunnerApp.Application.Services.AuthContext.Interfaces;

public interface IAuthService
{
    public Task<RegisterUserAccountServiceOutput> RegisterServiceAsync(
        RegisterUserAccountServiceInput input,
        CancellationToken cancellationToken);

    public Task<LoginUserAccountServiceOutput> LoginServiceAsync(
        LoginUserAccountServiceInput input,
        CancellationToken cancellationToken);

    public Task<RefreshTokenServiceOutput> RefreshTokenServiceAsync(
        RefreshTokenServiceInput input,
        CancellationToken cancellationToken);

    public Task LogoutServiceAsync(
        string userId,
        CancellationToken cancellationToken);

    public Task<IdentityResult> ChangePasswordAsync(
        ChangePasswordInput input,
        CancellationToken cancellationToken);
}
