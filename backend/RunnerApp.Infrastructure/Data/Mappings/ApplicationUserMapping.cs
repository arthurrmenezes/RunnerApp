using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Identity.Entities;

namespace RunnerApp.Infrastructure.Data.Mappings;

public sealed record ApplicationUserMapping : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasOne(u => u.Account)
            .WithOne()
            .HasForeignKey<ApplicationUser>(u => u.AccountId);

        #region Properties Configuration

        builder.Property(u => u.AccountId)
            .HasConversion(
                id => id.Id,
                value => IdValueObject.Factory(value))
            .IsRequired();

        #endregion
    }
}
