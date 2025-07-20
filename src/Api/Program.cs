using Api.Configurations;
using Api.Endpoints;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do EF Core
builder.Services.AddDbContext<GameCatalogDbContext>(options =>
    options.UseInMemoryDatabase("GameCatalog"));

// DI + Swagger
builder.Services.AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mapeamento de endpoints
app.GameEndpoints();

app.Run();