using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Infrastructure.Data.Mappings;

public sealed record TrainingMapping : IEntityTypeConfiguration<Training>
{
    public void Configure(EntityTypeBuilder<Training> builder)
    {
        builder.ToTable("trainings");

        builder.HasKey(p => p.Id);

        builder.HasOne(t => t.Account)
            .WithMany(a => a.Trainings)
            .HasForeignKey(t => t.AccountId)
            .IsRequired();

        #region Properties Configuration

        builder.Property(t => t.Id)
            .IsRequired()
            .HasConversion(t => t.Id, value => IdValueObject.Factory(value))
            .ValueGeneratedNever();

        builder.Property(t => t.AccountId)
            .IsRequired()
            .HasConversion(t => t.Id, value => IdValueObject.Factory(value));

        builder.Property(t => t.Location)
            .HasColumnName("location")
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(t => t.Distance)
            .HasColumnName("distance")
            .IsRequired();

        builder.Property(t => t.Duration)
            .HasColumnName("duration")
            .IsRequired();

        builder.Property(t => t.Date)
            .HasColumnName("date")
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        #endregion
    }
}
