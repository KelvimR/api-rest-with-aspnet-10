using api_rest_with_aspnet_10_tests.IntegrationTests.Tools;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace api_rest_with_aspnet_10_tests.IntegrationTests;

public class SwaggerIntegrationTests : IClassFixture<SqlServerFixture>
{
    private readonly HttpClient _client;
    public SwaggerIntegrationTests(SqlServerFixture fixture)
    {
        //Aqui nos conectamos com o banco
        var factory = new CustomWebApplicationFactory<Program>(fixture.ConnectionString);
        
        //Aqui criamos o client para conectar na api
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("http://localhost")
        });
    }

    //Aqui estamos validando se o Json é gerado
    [Fact]
    public async Task SwaggerJson_ShouldReturnSwaggerJson()
    {
        //Arrange & Act
        var response = await _client.GetAsync("/swagger/v1/swagger.json");
        response.EnsureSuccessStatusCode();
               
        //Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNull(); // Não pode ser null 
        content.Should().Contain("/api/person/v1");    
    }


    [Fact]
    public async Task SwaggerUI_ShouldReturnSwaggerUI()
    {
        //Arrange & Act
        var response = await _client.GetAsync("/swagger-ui/index.html");

        //Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("<div id=\"swagger-ui\">");
    }
}
