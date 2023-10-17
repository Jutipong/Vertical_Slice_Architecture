
using Api.Extensions;
using Api.Middleware;
using Application;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var sqlConnection = builder.Configuration.GetConnectionString("SqlServer")!;
builder.Services.AddDbContext<SqlContext>(o => o.UseSqlServer(sqlConnection));

var assembly = typeof(Program).Assembly;
builder.AddSwagger();
builder.Services.AddCors();
builder.Services.AddCarter();

builder.Services
    .AddApplication()
    .AddInfrastructure();

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
