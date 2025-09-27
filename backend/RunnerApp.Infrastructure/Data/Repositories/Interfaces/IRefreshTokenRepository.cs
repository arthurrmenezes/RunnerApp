using RunnerApp.Infrastructure.Identity.Entities;

namespace RunnerApp.Infrastructure.Data.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    public Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);

    public Task<RefreshToken?> GetRefreshTokenByJwtTokenAsync(string token, CancellationToken cancellationToken);

    public Task RevokeRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
}
