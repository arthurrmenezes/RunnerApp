using Microsoft.Extensions.DependencyInjection;
using RunnerApp.Application.Services.AccountContext;
using RunnerApp.Application.Services.AccountContext.Interfaces;
using RunnerApp.Application.Services.AuthContext;
using RunnerApp.Application.Services.AuthContext.Interfaces;
using RunnerApp.Application.Services.TrainingContext;
using RunnerApp.Application.Services.TrainingContext.Interfaces;

namespace RunnerApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection ApplyApplicationDependenciesConfiguration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITrainingService, TrainingService>();
        serviceCollection.AddScoped<IAccountService, AccountService>();
        serviceCollection.AddScoped<IAuthService, AuthService>();

        return serviceCollection;
    }
}
