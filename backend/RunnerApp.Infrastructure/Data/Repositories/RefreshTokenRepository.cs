using Microsoft.EntityFrameworkCore;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Data.Repositories.Interfaces;
using RunnerApp.Infrastructure.Identity.Entities;

namespace RunnerApp.Infrastructure.Data.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly DataContext _dataContext;

    public RefreshTokenRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        await _dataContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<RefreshToken?> GetRefreshTokenByJwtTokenAsync(string token, CancellationToken cancellationToken)
    {
        return await _dataContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
    }

    public async Task RevokeRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        refreshToken.Revoke();
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RevokeAllTokensByUserIdAsync(IdValueObject userId, CancellationToken cancellationToken)
    {
        var tokens = await _dataContext.RefreshTokens
            .Where(rt => rt.UserId == userId 
                && rt.RevokedAt == null
                && rt.ExpirationDate > DateTime.UtcNow)
            .ToListAsync(cancellationToken);

        foreach (var token in tokens)
            token.Revoke();

        await _dataContext.SaveChangesAsync(cancellationToken);
    }
}
