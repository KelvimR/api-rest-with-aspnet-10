using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.OpenApi;

namespace api_rest_with_aspnet_10.Configurations
{
    public static class OpenAPIConfig
    {
        private static readonly string AppName = "API REST with ASP.NET 10";
        private static readonly string AppDescription = "API REST with ASP.NET 10 - Course of Udemy";

        public static IServiceCollection AddOpenAPIConfig(this IServiceCollection services)
        {
            //AddSingleton: Registra um serviço como singleton, ou seja, uma única instância é criada e compartilhada em toda a aplicação.
            services.AddSingleton(new OpenApiInfo
            {
                Title = AppName,
                Version = "v1",
                Description = AppDescription,
                Contact = new OpenApiContact
                {
                    Name = "Kelvim Rodrigues",
                    Email = "kelvimrodrigues1@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/kelvim-rodrigues-dev")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            return services;
        }
    }
}
