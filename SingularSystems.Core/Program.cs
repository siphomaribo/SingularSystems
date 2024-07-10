using Microsoft.Extensions.Options;
using SingluarSystems.ExternalServices.HttpProductAPI;
using SingluarSystems.ExternalServices.Interfaces;
using SingluarSystems.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<SalesAPISettingsModel>(builder.Configuration.GetSection("SalesAPISettingsModel"));

builder.Services.AddHttpClient<IProductRepository, SalesAPIService>((serviceProvider, client) =>
{
    var apiSettings = serviceProvider.GetRequiredService<IOptions<SalesAPISettingsModel>>().Value;
    client.BaseAddress = new Uri(apiSettings.ProductApiBaseUrl);
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
