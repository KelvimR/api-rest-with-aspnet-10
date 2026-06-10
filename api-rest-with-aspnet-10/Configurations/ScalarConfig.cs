using Scalar.AspNetCore;

namespace api_rest_with_aspnet_10.Configurations;

public static class ScalarConfig
{
    //Config Scalar 
    private static readonly string AppName = "API REST with ASP.NET 10";

    public static WebApplication UseScalarSpecification(this WebApplication app)
    {
        app.MapScalarApiReference("/scalar", options =>
        {
            options
                .WithTitle(AppName)
                .WithOpenApiRoutePattern("/swagger/v1/swagger.json");
        });

        return app;
    }
}
