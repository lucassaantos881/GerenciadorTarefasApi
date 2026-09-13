using GerenciadorTarefasApi.Context;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using GerenciadorTarefasApi.Services;
using GerenciadorTarefasApi.Repository;
using System.Text.Json.Serialization;
using Serilog;
using GerenciadorTarefasApi.Middleware;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() //mostra no console
    .WriteTo.File("logs/log.txt", rollingInterval : RollingInterval.Day) //salva em arquivo de log a cada dia
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers().AddJsonOptions(options => 
    options.JsonSerializerOptions.
    ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();

//Qualquer tipo que implemente IGerenciadorRepository<T> será resolvido para a implementação IGerenciadorRepository<T>
builder.Services.AddScoped(typeof(IGerenciadorRepository<>), typeof(GerenciadorRepository<>));

builder.Services.AddScoped<ITarefaService, TarefaService>();
builder.Services.AddScoped<IProjetoService, ProjetoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();


string postgreConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Host.UseSerilog();

builder.Services.AddDbContext<GerenciadorContext>(options =>
        options.UseNpgsql(postgreConnection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}

//Configura o middleware de tratamento de exceções para capturar erros e retornar respostas apropriadas
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
