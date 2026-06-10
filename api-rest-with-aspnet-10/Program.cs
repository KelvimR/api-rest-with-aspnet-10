using api_rest_with_aspnet_10.Configurations;
using api_rest_with_aspnet_10.Repositories;
using api_rest_with_aspnet_10.Repositories.Implementations;
using api_rest_with_aspnet_10.Services;
using api_rest_with_aspnet_10.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddContentNegociation(); //Precisa ser iniciado junto ao controllers para que o Content Negociation funcione corretamente

//Dependency Injection
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddEvolveConfiguration(builder.Configuration, builder.Environment);
builder.Services.AddScoped<IPersonService, PersonServicesImpl>();

//builder.Services.AddScoped<IPersonRepository, PersonRepository>(); // Removido para usar repository genérico => Desafio curso
builder.Services.AddScoped<IBookService, BookServicesImpl>();
//builder.Services.AddScoped<IBookRepository, BookRepository>(); // Removido para usar repository genérico
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
