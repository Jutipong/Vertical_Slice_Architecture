using Microsoft.EntityFrameworkCore;
using Vertical_Slice_Architecture.Database;
using Vertical_Slice_Architecture.Features.Articles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(o =>
o.UseSqlServer(builder.Configuration.GetConnectionString(builder.Configuration.GetConnectionString("SqlServer")!)));

// Register MediatR
builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

CreateArticle.MapEndpoint(app);

app.UseHttpsRedirection();
app.Run();