using Microsoft.EntityFrameworkCore;
using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;
using RunnerApp.Infrastructure.Data.Mappings;

namespace RunnerApp.Infrastructure.Data;

public sealed class DataContext : DbContext
{
    public DbSet<Training> Trainings { get; set; }

    public DataContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TrainingMapping());
        base.OnModelCreating(modelBuilder);
    }
}
