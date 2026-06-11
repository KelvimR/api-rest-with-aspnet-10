using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Data.SqlTypes;
using System.Runtime.Intrinsics.X86;

namespace api_rest_with_aspnet_10_tests.IntegrationTests.Tools;

//Porque estamos fazendo isso?
//O que é: WebApplicationFactory personalizada para testes de integração que injeta uma connection string no config da aplicação de teste.
//Como funciona: Sobrepõe ConfigureWebHost e chama ConfigureAppConfiguration, adicionando uma coleção em memória com a chave "ConnectionStrings:DefaultConnection".
//Objetivo: Permitir executar a API em um host de teste com uma connection string controlada(ex.: banco de teste, SQLite in-memory).
public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class 
{
    private readonly string _connectionString;
    public CustomWebApplicationFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var dict = new Dictionary<string, string>
            {
                {
                    "ConnectionStrings:DefaultConnection",
                    _connectionString
                }
            };

            config.AddInMemoryCollection(dict!);
        });
    }
}
