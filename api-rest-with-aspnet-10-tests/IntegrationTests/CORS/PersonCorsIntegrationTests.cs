using api_rest_with_aspnet_10.Data.DTO.V1;
using api_rest_with_aspnet_10_tests.IntegrationTests.Tools;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using static api_rest_with_aspnet_10_tests.IntegrationTests.Tools.PriorityOrder;

namespace api_rest_with_aspnet_10_tests.IntegrationTests.CORS;

[TestCaseOrderer("api_rest_with_aspnet_10_tests.IntegrationTests.Tools.PriorityOrder", "api_rest_with_aspnet_10_tests")]
public class PersonCorsIntegrationTests : IClassFixture<SqlServerFixture>
{
    private readonly HttpClient _client;
    private static PersonDTO _person; 

    public PersonCorsIntegrationTests(SqlServerFixture fixture)
    {
        //Aqui nos conectamos com o banco
        var factory = new CustomWebApplicationFactory<Program>(fixture.ConnectionString);

        //Aqui criamos o client para conectar na api
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("http://localhost")
        });
    }

    private void AddOriginHeader(string origin)
    {
        _client.DefaultRequestHeaders.Remove("Origin");
        _client.DefaultRequestHeaders.Add("Origin", origin);
    }

    [Fact(DisplayName = "01 - Create Person With Allowed Origin")]
    [TestPriorityAttibute(1)]
    public async Task CreatePerson_WithAllowedOrigin_ShouldReturnCreated()
    {
        //Arrange
        AddOriginHeader("http://localhost:3000");
        var request = new PersonDTO
        {
            FirstName = "Kelvim",
            LastName = "Rodrigues",
            Address = "R. Artur Momberger",
            Gender = "Male"
        };

        // Act
        var response = await _client.PostAsJsonAsync("api/person/v1", request);

        //Assert
        response.EnsureSuccessStatusCode();

        var createdPerson = await response.Content.ReadFromJsonAsync<PersonDTO>();
        createdPerson.Id.Should().BeGreaterThan(0);

        _person = createdPerson;

    }

    //Corrigir esse teste
    //[Fact(DisplayName = "02 - Create Person With Disallowed Origin")]
    //[TestPriorityAttibute(2)]
    //public async Task CreatePerson_WithDisallowedOrigin_ShouldReturnForbidden()
    //{
    //    //Arrange
    //    AddOriginHeader("https://kelvimsantos.com.br");
    //    var request = new PersonDTO
    //    {
    //        FirstName = "Kelvim",
    //        LastName = "Rodrigues",
    //        Address = "R. Artur Momberger",
    //        Gender = "Male"
    //    };

    //    // Act
    //    var response = await _client.PostAsJsonAsync("api/person/v1", request);

    //    //Assert
    //    response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

    //    var content = response.Content.ReadAsStringAsync();
    //    content.Should().Be("CORS origin not allowed");
    //}
}
