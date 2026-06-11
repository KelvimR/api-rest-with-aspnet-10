using api_rest_with_aspnet_10_tests.IntegrationTests.Tools;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;

namespace api_rest_with_aspnet_10_tests.IntegrationTests;

public class ScalarIntegrationsTests : IClassFixture<SqlServerFixture>
{
    private readonly HttpClient _client;
    public ScalarIntegrationsTests(SqlServerFixture fixture)
    {
        //Aqui nos conectamos com o banco
        var factory = new CustomWebApplicationFactory<Program>(fixture.ConnectionString);

        //Aqui criamos o client para conectar na api
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("http://localhost")
        });
    }

    [Fact]
    public async Task ScalarUI_ShouldReturnScalarUI()
    {
        //Arrange & Act
        var response = await _client.GetAsync("/scalar/");

        //Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("<title>API REST with ASP.NET 10</title>");
        content.Should().Contain("script src");
    }
}
