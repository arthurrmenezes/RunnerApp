using RunnerApp.Application;
using RunnerApp.Infrastructure;

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

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
