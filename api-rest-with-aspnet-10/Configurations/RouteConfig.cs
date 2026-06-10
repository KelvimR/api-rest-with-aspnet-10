using System.Runtime.CompilerServices;

namespace api_rest_with_aspnet_10.Configurations;

public static class RouteConfig
{
    //Aqui estamos colocando a string como minuscula para que as rotas da API sejam todas em minúsculas.
    //o que é uma boa prática para evitar problemas de case-sensitive nas rotas e também para manter a consistência das rotas da API.
    public static IServiceCollection AddRouteConfig(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseQueryStrings = true;
            options.LowercaseUrls = true;
        });

        return services;
    }
}
