using Serilog;

namespace api_rest_with_aspnet_10.Configurations
{
    public static class LoggingConfig
    {
        public static void AddSeriLogLogging(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Debug()
                .CreateLogger();
            builder.Host.UseSerilog();
        }
    }
}
