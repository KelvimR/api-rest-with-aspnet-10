using api_rest_with_aspnet_10.Configurations;
using Testcontainers.MsSql;

namespace api_rest_with_aspnet_10_tests.IntegrationTests.Tools;

public class SqlServerFixture : IAsyncLifetime
{
    public MsSqlContainer Container { get; } // Com esse cara conseguimos criar um container com SQLSERVER
    public string ConnectionString => Container.GetConnectionString();

    //Definimos as configuracoes em runtime
    public SqlServerFixture()
    {
        Container = new MsSqlBuilder().Build();
            //.WithPassword("123@") //Posso colocar este parametro, no meu caso não tenho           
    }

    public async Task InitializeAsync()
    {
        //inicializa o container
        await Container.StartAsync();
        //Assim conseguimos restaurar o banco para os testes com os dados minimos
        EvolveConfig.ExecuteMigrations(ConnectionString);
    }
    public async Task DisposeAsync()
    {
        //destroi o container
        await Container.DisposeAsync();
    }
}
