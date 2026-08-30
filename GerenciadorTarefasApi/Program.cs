using GerenciadorTarefasApi.Context;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using GerenciadorTarefasApi.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => 
    options.JsonSerializerOptions.
    ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ITarefaService, TarefaService>();
builder.Services.AddScoped<IProjetoService, ProjetoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

string postgreConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<GerenciadorContext>(options =>
        options.UseNpgsql(postgreConnection));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
