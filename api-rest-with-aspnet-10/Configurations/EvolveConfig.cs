using EvolveDb;
using Serilog;
using Microsoft.Data.SqlClient;

namespace api_rest_with_aspnet_10.Configurations;

public static class EvolveConfig
{
    public static IServiceCollection AddEvolveConfiguration(
      this IServiceCollection services,
      IConfiguration configuration,
      IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];

            if (String.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Conexão com banco de dados não foi estabelecida.");

            try
            {
                ExecuteMigrations(connectionString);               
            }
            catch (Exception ex)
            {
                Log.Error("Evolve migration failed", ex);
                throw;
            }
        }

        return services;

    }

    //Aqui conseguimos chamar via fixture parar testes de integracao
    public static void ExecuteMigrations(string connectionString)
    {
        using var evolveConnection = new SqlConnection(connectionString);
        var evolve = new Evolve(evolveConnection, msg => Log.Information(msg))
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true
        };
        evolve.Migrate();
    }
}
