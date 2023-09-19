using Vertical_Slice_Architecture.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(o =>
o.UseSqlServer(builder.Configuration.GetConnectionString(builder.Configuration.GetConnectionString("SqlServer")!)));

var assembly = typeof(Program).Assembly;

// Register MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

// Register Carter
builder.Services.AddCarter();

// Register FluentValidation
builder.Services.AddValidatorsFromAssembly(assembly);

// app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();

//CreateArticle.MapEndpoint(app);
app.UseHttpsRedirection();
app.Run();