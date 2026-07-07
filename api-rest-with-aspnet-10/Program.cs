using api_rest_with_aspnet_10.Auth.Contract;
using api_rest_with_aspnet_10.Auth.Tools;
using api_rest_with_aspnet_10.Configurations;
using api_rest_with_aspnet_10.Repositories;
using api_rest_with_aspnet_10.Repositories.Implementations;
using api_rest_with_aspnet_10.Services;
using api_rest_with_aspnet_10.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddContentNegociation(); //Precisa ser iniciado junto ao controllers para que o Content Negociation funcione corretamente

builder.Services.AddEndpointsApiExplorer(); // Aqui é necessário para o Swagger/scalar funcionar, pois ele precisa de um endpoint para gerar a documentação
builder.Services.AddOpenAPIConfig(); // Configuração do OpenAPI/Swagger, pode ser usado tanto pelo swagger quanto pelo scalar, pois ambos usam o OpenAPI para gerar a documentação
builder.Services.AddSwaggerConfig(); // Configuração do Swagger, que é a ferramenta que gera a documentação da API e também fornece uma interface gráfica para testar os endpoints da API
builder.Services.AddRouteConfig(); // Configuração das rotas da API, onde podemos definir as rotas personalizadas para os nossos endpoints, como por exemplo, definir um prefixo para todas as rotas ou definir uma rota específica para um endpoint
builder.Services.AddCorsConfiguration(builder.Configuration);

//Dependency Injection
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddEvolveConfiguration(builder.Configuration, builder.Environment);
builder.Services.AddAuthConfiguration(builder.Configuration);

builder.Services.AddScoped<IPasswordHasher, Sha256PasswordHasher>();
builder.Services.AddScoped<IUserAuthService, UserAuthServicesImpl>();
builder.Services.AddScoped<ILoginService, LoginServiceImpl>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IPersonService, PersonServicesImpl>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IBookService, BookServicesImpl>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UserCorsConfiguration();

app.MapControllers();

app.UseSwaggerSpecification(); // Configuração do middleware do Swagger para ser usado na aplicação, ou seja, estou dizendo para a aplicação usar o Swagger para gerar a documentação da API e também estou configurando o endpoint do Swagger para acessar a documentação gerada.
app.UseScalarSpecification();  // So declaramos aqui que queremos utilizar, pois OpenAPI já está declarado

app.Run();
