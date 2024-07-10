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

        public SalesAPIService(HttpClient httpClient, IOptions<SalesAPISettingsModel> apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings;
        }
        public async Task<IEnumerable<ProductSaleModel>> GetProductSalesSummaryAsync(int productId)
        {
            if (string.IsNullOrWhiteSpace(_apiSettings.Value.ProductApiBaseUrl))
            {
                throw new InvalidOperationException("Product API base URL is not configured.");
            }

            if (productId <= 0)
            {
                throw new ArgumentException("Product ID must be a positive integer.", nameof(productId));
            }

            var url = $"{_apiSettings.Value.ProductApiBaseUrl}/product-sales?Id={productId}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var salesSummary = await response.Content.ReadFromJsonAsync<IEnumerable<ProductSaleModel>>();
            return salesSummary;
        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            if (string.IsNullOrWhiteSpace(_apiSettings.Value.ProductApiBaseUrl))
            {
                throw new InvalidOperationException("Product API base URL is not configured.");
            }

            var response = await _httpClient.GetAsync($"{_apiSettings.Value.ProductApiBaseUrl}/products");
            response.EnsureSuccessStatusCode();

            var products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductModel>>();
            return products;
        }
    }
}
