using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Identity.Entities;

namespace RunnerApp.Infrastructure.Data.Mappings;

public sealed record RefreshTokenMapping : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        #region Properties Configuration

        builder.Property(rt => rt.Id)
            .HasConversion(
                id => id.Id,
                value => IdValueObject.Factory(value))
            .IsRequired();

        builder.Property(rt => rt.Token)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(rt => rt.UserId)
            .IsRequired();

        builder.Property(rt => rt.ExpirationDate)
            .IsRequired();

        builder.Property(rt => rt.RevokedAt)
            .IsRequired(false);

        builder.Property(rt => rt.CreatedAt)
            .IsRequired();

        #endregion

        builder.HasIndex(rt => rt.Token).IsUnique();
    }
}
