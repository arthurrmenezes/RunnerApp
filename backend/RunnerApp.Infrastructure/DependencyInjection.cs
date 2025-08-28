using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RunnerApp.Infrastructure.Data;
using RunnerApp.Infrastructure.Data.Repositories;
using RunnerApp.Infrastructure.Data.Repositories.Interfaces;

namespace RunnerApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ApplyInfrastructureDependenciesConfiguration(
        this IServiceCollection serviceCollection,
        string connectionString)
    {
        #region DbContext Configuration

        serviceCollection.AddDbContext<DataContext>(
            optionsAction: p => p.UseNpgsql(
                connectionString: connectionString,
                npgsqlOptionsAction: p => p.MigrationsAssembly("RunnerApp.Infrastructure")),
            contextLifetime: ServiceLifetime.Scoped,
            optionsLifetime: ServiceLifetime.Scoped);

        #endregion

        #region Repositories Configuration

        serviceCollection.AddScoped<ITrainingRepository, TrainingRepository>();

        #endregion

        return serviceCollection;
    }
}
