using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Infrastructure.Identity.Entities;

public class RefreshToken
{
    public IdValueObject Id { get; private set; }
    public string Token { get; private set; }
    public Guid UserId { get; private set; }
    public ApplicationUser User { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private RefreshToken() { }

    private RefreshToken(string token, IdValueObject userId, ApplicationUser user, DateTime expirationDate)
    {
        Id = IdValueObject.New();
        Token = token;
        UserId = userId.Id;
        User = user;
        ExpirationDate = expirationDate;
        RevokedAt = null;
        CreatedAt = DateTime.UtcNow;
    }

    public static RefreshToken Factory(string token, IdValueObject userId, ApplicationUser user, DateTime expirationDate)
        => new RefreshToken(token, userId, user, expirationDate);

    public bool IsExpired() => DateTime.UtcNow >= ExpirationDate;
    public bool IsRevoked() => RevokedAt.HasValue;
    public void Revoke()
    {
        RevokedAt = DateTime.UtcNow;
    }
}
