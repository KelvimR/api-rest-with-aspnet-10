using Microsoft.OpenApi;

namespace api_rest_with_aspnet_10.Configurations;

public static class SwaggerConfig
{
    private static readonly string AppName = "API REST with ASP.NET 10";
    private static readonly string AppDescription = "API REST with ASP.NET 10 - Course of Udemy";
    
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
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
            
            // Evita conflitos de nomes de classes na documentação do Swagger, usando o nome completo da classe (incluindo namespace) como identificador único.
            options.CustomSchemaIds(type => type.FullName); 

        });

        return services;
    }

    //Aqui estou configurando o middleware do Swagger para ser usado na aplicação, ou seja, estou dizendo para a aplicação usar o Swagger para gerar a documentação da API e também estou configurando o endpoint do Swagger para acessar a documentação gerada.
    public static IApplicationBuilder UseSwaggerSpecification(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = "swagger-ui"; // Define o prefixo da rota para acessar a interface do Swagger UI, ou seja, a interface gráfica onde é possível visualizar a documentação da API e testar os endpoints. Com essa configuração, a interface do Swagger UI estará disponível em http://localhost:porta/Swagger%20ui.
            options.DocumentTitle = AppName;// Define o título da página da interface do Swagger UI, que será exibido na aba do navegador quando a interface for acessada.
        });

        return app;
    }


}