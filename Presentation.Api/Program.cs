
using Api.Extensions;
using Api.Middleware;
using Application;
using Domain;
using Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var _config = config.GetSection(nameof(AppSettings)).Get<AppSettings>()!;
builder.Services.AddSingleton(_config);

var assembly = typeof(Program).Assembly;
builder.AddSwagger();
builder.Services.AddCors();
builder.Services.AddCarter();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerEndpoints();
}

app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapCarter();
app.UseHttpsRedirection();
app.Run();
