using System.Text.Json.Serialization;
using RunnerApp.Application;
using RunnerApp.Infrastructure;
using RunnerApp.WebApi.Middlewares;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Infrastructure Dependencies Configuration

        const string connectionString = "Dependencies:Infrastructure:Data:PostgreSQLConnectionString";

        builder.Services.ApplyInfrastructureDependenciesConfiguration(
            connectionString: builder.Configuration[connectionString]!);

        #endregion

        #region Application Dependencies Configuration

        builder.Services.ApplyApplicationDependenciesConfiguration();

        #endregion

        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseHttpsRedirection();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        app.Run();
    }
}
