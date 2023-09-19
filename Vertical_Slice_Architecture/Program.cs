using Vertical_Slice_Architecture.Extensions.Swagger;

var builder = WebApplication.CreateBuilder(args);

var sqlConnection = builder.Configuration.GetConnectionString("SqlServer")!;
builder.Services.AddDbContext<SqlContext>(o => o.UseSqlServer(sqlConnection));

var assembly = typeof(Program).Assembly;

builder.AddSwagger();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
builder.Services.AddCors();
builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssembly(assembly);

// app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerEndpoints();
}

app.MapCarter();
app.UseHttpsRedirection();
app.Run();