using System.Globalization;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Application.Interfaces;
using PasswordManager.Infrastructure.Data;
using PasswordManager.Infrastructure.Services;
using PasswordManager_API;

var builder = WebApplication.CreateBuilder(args);

//CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
//CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

// Retrieve and log the connection string to ensure it's loaded correctly
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DbContext for SQL Server
builder.Services.AddDbContext<PasswordManagerContext>(options =>
    options.UseSqlServer(connectionString));

// Add application repository
builder.Services.AddScoped<IPasswordRepository, PasswordRepository>();

// Add application services
builder.Services.AddScoped<IPasswordService, PasswordService>();


// Add controllers and Swagger for API documentation
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Handle OPTIONS requests before they reach controllers
app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Options)
    {
        context.Response.StatusCode = StatusCodes.Status204NoContent;
        return;
    }

    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
