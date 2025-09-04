using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunnerApp.Domain.BoundedContexts.AccountContext.Entities;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Infrastructure.Data.Mappings;

public sealed record AccountMapping : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("accounts");

        builder.HasKey(p => p.Id);

        builder.HasMany(p => p.Trainings)
            .WithOne(t => t.Account)
            .HasForeignKey(t => t.AccountId)
            .IsRequired();

        #region Properties Configuration

        builder.Property(t => t.Id)
            .IsRequired()
            .HasConversion(t => t.Id, value => IdValueObject.Factory(value))
            .ValueGeneratedNever();

        builder.Property(p => p.FirstName)
            .HasColumnName("first_name")
            .IsRequired()
            .HasMaxLength(100)
            .HasConversion(
                firstName => firstName.ToString(),
                value => FirstNameValueObject.Factory(value));

        builder.Property(p => p.Surname)
            .HasColumnName("surname")
            .IsRequired()
            .HasMaxLength(100)
            .HasConversion(
                surname => surname.ToString(),
                value => SurnameValueObject.Factory(value));

        builder.Property(p => p.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(255)
            .HasConversion(
                email => email.ToString(),
                value => EmailValueObject.Factory(value));

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        #endregion
    }
}
