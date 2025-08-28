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

        #region Properties Configuration

        builder.Property(t => t.Id)
            .IsRequired(true)
            .HasConversion(t => t.Id, value => IdValueObject.Factory(value))
            .ValueGeneratedNever();
        builder.Property(t => t.Location)
            .HasColumnName("location")
            .IsRequired(true)
            .HasConversion<string>()
            .HasMaxLength(50)
            .ValueGeneratedNever();
        builder.Property(t => t.Distance)
            .HasColumnName("distance")
            .IsRequired(true)
            .ValueGeneratedNever();
        builder.Property(t => t.Duration)
            .HasColumnName("duration")
            .IsRequired(true)
            .ValueGeneratedNever();
        builder.Property(t => t.Date)
            .HasColumnName("date")
            .IsRequired(true)
            .ValueGeneratedNever();
        builder.Property(t => t.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired(true);

        #endregion
    }
}
