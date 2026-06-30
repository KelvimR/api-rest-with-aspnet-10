namespace api_rest_with_aspnet_10.Configurations;

public static class CorsConfig
{
    public static void AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        //Aqui estamos pegando o config do appsettings para parametrização global
        var origins = configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>();

        services.AddCors(options =>
        {
            // assim politicas especificas
            options.AddPolicy("LocalPolicy",
                policy => policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            options.AddPolicy("MultiplePolicy",
                policy => policy.WithOrigins(
                    "http://localhost:3000",
                    "http://localhost:8080",
                    "http://localhost:8081")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            //Podemos utilizar assim globalmente
            options.AddPolicy("DefaultPolicy",
               policy => policy.WithOrigins(origins)
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials());
        });
    }

    public static IApplicationBuilder UserCorsConfiguration(this IApplicationBuilder app)
    {
        app.UseCors();
        return app;
    }
}
