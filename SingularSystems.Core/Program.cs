using Microsoft.Extensions.Options;
using SingluarSystems.Abstraction.Interface;
using SingluarSystems.ExternalServices.HttpProductAPI;
using SingluarSystems.ExternalServices.Interfaces;
using SingluarSystems.Models;
using SingluarSystems.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<SalesAPISettingsModel>(builder.Configuration.GetSection("SalesAPISettingsModel"));

builder.Services.AddHttpClient<IProductRepository, SalesAPIService>((serviceProvider, client) =>
{
    var apiSettings = serviceProvider.GetRequiredService<IOptions<SalesAPISettingsModel>>().Value;
    client.BaseAddress = new Uri(apiSettings.ProductApiBaseUrl);
});

builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add logging configuration
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders(); 
    logging.AddConsole();
    logging.AddDebug(); 
                        // Add other logging configurations as needed (e.g., file logging, Azure Application Insights)
});

builder.Services.AddSwaggerGen();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
