// Program setup and configuration for ASP.NET Core application
using Microsoft.Extensions.Options;
using SingluarSystems.Abstraction.Interface;
using SingluarSystems.ExternalServices.HttpProductAPI;
using SingluarSystems.ExternalServices.Interfaces;
using SingluarSystems.Models;
using SingluarSystems.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Sales API settings from configuration
builder.Services.Configure<SalesAPISettingsModel>(builder.Configuration.GetSection("SalesAPISettingsModel"));

// Configure HttpClient for SalesAPIService
builder.Services.AddHttpClient<IProductRepository, SalesAPIService>((serviceProvider, client) =>
{
    var apiSettings = serviceProvider.GetRequiredService<IOptions<SalesAPISettingsModel>>().Value;
    client.BaseAddress = new Uri(apiSettings.ProductApiBaseUrl);
});

// Register ProductService as a scoped service
builder.Services.AddScoped<IProductService, ProductService>();

// Add controllers to the service container
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add Swagger for API documentation
builder.Services.AddSwaggerGen();

// Configure logging
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders(); // Clear default providers
    logging.AddConsole(); // Add console logging
    logging.AddDebug(); // Add debug logging
    // Add other logging configurations as needed (e.g., file logging, Azure Application Insights)
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS to allow any origin, method, and header
app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Enable authorization middleware
app.UseAuthorization();

// Map controller routes
app.MapControllers();

// Run the application
app.Run();
