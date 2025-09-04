using Microsoft.Extensions.DependencyInjection;
using RunnerApp.Application.Services.AccountContext;
using RunnerApp.Application.Services.AccountContext.Interfaces;
using RunnerApp.Application.Services.TrainingContext;
using RunnerApp.Application.Services.TrainingContext.Interfaces;

namespace RunnerApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection ApplyApplicationDependenciesConfiguration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITrainingService, TrainingService>();
        serviceCollection.AddScoped<IAccountService, AccountService>();

        return serviceCollection;
    }
}
