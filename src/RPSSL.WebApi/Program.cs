using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using RPSSL.Application;
using RPSSL.Domain.Abstractions;
using RPSSL.Domain.Implementations;
using RPSSL.Infrastructure;
using RPSSL.Presentation;
using RPSSL.WebApi.Extensions;
using Serilog;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IPlayService>(new PlayService());

builder.Services.AddControllers()
    .AddApplicationPart(RPSSL.Presentation.AssemblyRef.Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RPSSL Web Api",
        Description = "Rock, Paper, Scissors, Spock, Lizard game - sample Web Api"
    });

    foreach (var filePath in System.IO.Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!), "*.xml"))
    {
        options.IncludeXmlComments(filePath);
    }
});

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

app.MapHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.UseExceptionHandler();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();

public partial class Program { }
