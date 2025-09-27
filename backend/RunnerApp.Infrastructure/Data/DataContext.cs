using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RunnerApp.Domain.BoundedContexts.AccountContext.Entities;
using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;
using RunnerApp.Infrastructure.Data.Mappings;
using RunnerApp.Infrastructure.Identity.Entities;

namespace RunnerApp.Infrastructure.Data;

public sealed class DataContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public DbSet<Training> Trainings { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DataContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TrainingMapping());
        modelBuilder.ApplyConfiguration(new AccountMapping());
        modelBuilder.ApplyConfiguration(new ApplicationUserMapping());
        modelBuilder.ApplyConfiguration(new RefreshTokenMapping());
        base.OnModelCreating(modelBuilder);
    }
}
