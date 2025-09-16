﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RunnerApp.Infrastructure.Data;
using RunnerApp.Infrastructure.Data.Repositories;
using RunnerApp.Infrastructure.Data.Repositories.Interfaces;
using RunnerApp.Infrastructure.Identity;

namespace RunnerApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ApplyInfrastructureDependenciesConfiguration(
        this IServiceCollection serviceCollection,
        IConfiguration configuration,
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

        #region Identity Configuration

        serviceCollection
            .AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -_";
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 3;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

        serviceCollection
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSettings:PrivateKey"]!))
                };
            });
        serviceCollection.AddAuthorization();

        serviceCollection.AddScoped<TokenGenerator>();

        #endregion

        #region Repositories Configuration

        serviceCollection.AddScoped<ITrainingRepository, TrainingRepository>();
        serviceCollection.AddScoped<IAccountRepository, AccountRepository>();

        #endregion

        return serviceCollection;
    }
}
