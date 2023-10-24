using MyFirstAPI.Models;
using Microsoft.EntityFrameworkCore;
using Asp.Versioning;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShopContext>(options =>
{
    options.UseInMemoryDatabase("Shop"); // Install-Package Microsoft.EntityFrameworkCore.InMemory to make it work 
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true; // Reports the API versions in the response header    
    options.DefaultApiVersion = new ApiVersion(1, 0); // Sets the default API version to 1.0
    options.AssumeDefaultVersionWhenUnspecified = true; // Sets the default API version to 1.0

    options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version"); // Sets the API version reader to read the API version from the X-API-Version header
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        
        builder.WithHeaders("X-API-Version");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
