// Service class to interact with external Product API
using Microsoft.Extensions.Options;
using SingluarSystems.ExternalServices.Interfaces;
using SingluarSystems.Models;
using System.Net.Http.Json;

namespace SingluarSystems.ExternalServices.HttpProductAPI
{
    public class SalesAPIService : IProductRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<SalesAPISettingsModel> _apiSettings;

        // Constructor to initialize HttpClient and API settings
        public SalesAPIService(HttpClient httpClient, IOptions<SalesAPISettingsModel> apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings;
        }

        // Method to fetch product sales summary from the external API
        public async Task<IEnumerable<ProductSaleModel>> GetProductSalesSummaryAsync(int productId)
        {
            // Ensure API base URL is configured
            if (string.IsNullOrWhiteSpace(_apiSettings.Value.ProductApiBaseUrl))
            {
                throw new InvalidOperationException("Product API base URL is not configured.");
            }

            // Validate productId
            if (productId <= 0)
            {
                throw new ArgumentException("Product ID must be a positive integer.", nameof(productId));
            }

            // Construct the URL and make the API call
            var url = $"{_apiSettings.Value.ProductApiBaseUrl}/product-sales?Id={productId}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Deserialize and return the sales summary data
            var salesSummary = await response.Content.ReadFromJsonAsync<IEnumerable<ProductSaleModel>>();
            return salesSummary;
        }

        // Method to fetch all products from the external API
        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            // Ensure API base URL is configured
            if (string.IsNullOrWhiteSpace(_apiSettings.Value.ProductApiBaseUrl))
            {
                throw new InvalidOperationException("Product API base URL is not configured.");
            }

            // Make the API call to fetch products
            var response = await _httpClient.GetAsync($"{_apiSettings.Value.ProductApiBaseUrl}/products");
            response.EnsureSuccessStatusCode();

            // Deserialize and return the products data
            var products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductModel>>();
            return products;
        }
    }
}
