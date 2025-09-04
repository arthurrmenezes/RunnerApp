using Microsoft.EntityFrameworkCore;
using RunnerApp.Domain.BoundedContexts.AccountContext.Entities;
using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;
using RunnerApp.Infrastructure.Data.Mappings;

namespace RunnerApp.Infrastructure.Data;

public sealed class DataContext : DbContext
{
    public DbSet<Training> Trainings { get; set; }
    public DbSet<Account> Accounts { get; set; }

    public DataContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TrainingMapping());
        modelBuilder.ApplyConfiguration(new AccountMapping());
        base.OnModelCreating(modelBuilder);
    }
}
