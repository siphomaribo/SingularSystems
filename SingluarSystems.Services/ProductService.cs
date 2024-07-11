using SingluarSystems.Abstraction.Interface;
using SingluarSystems.ExternalServices.Interfaces;
using SingluarSystems.Models;

namespace SingluarSystems.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        // Constructor to initialize the product repository
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Method to calculate sales summary based on product sales data
        public SalesSummaryModel CalculateSalesSummary(IEnumerable<ProductSaleModel> productSales)
        {
            // Ensure productSales is not null
            if (productSales == null)
            {
                throw new ArgumentNullException(nameof(productSales));
            }

            // Calculate total sale price by summing sale prices multiplied by quantities
            decimal totalSalePrice = productSales.Sum(s => s.SalePrice * s.SaleQty);
            // Calculate total sale quantity by summing all quantities
            int totalSaleQty = productSales.Sum(s => s.SaleQty);

            // Get the earliest and latest sale dates
            DateTime earliestSaleDate = productSales.Min(s => s.SaleDate.Value);
            DateTime latestSaleDate = productSales.Max(s => s.SaleDate.Value);
            // Calculate the number of days between the earliest and latest sale dates
            int daysToSell = (int)(latestSaleDate - earliestSaleDate).TotalDays + 1;

            // Calculate the average sale price per unit
            decimal averageSalePricePerUnit = totalSalePrice / totalSaleQty;

            // Find the maximum and minimum sale prices
            decimal maxSalePrice = productSales.Max(s => s.SalePrice);
            decimal minSalePrice = productSales.Min(s => s.SalePrice);

            // Format the date range string
            string dateRange = $"{earliestSaleDate.ToShortDateString()} - {latestSaleDate.ToShortDateString()}";

            // Create and set the sales summary model
            var summary = new SalesSummaryModel();
            summary.SetSummary(totalSalePrice, totalSaleQty, daysToSell, averageSalePricePerUnit, maxSalePrice, minSalePrice, dateRange);

            return summary;
        }

        // Method to get product sales asynchronously based on product ID
        public async Task<IEnumerable<ProductSaleModel>> GetProductSalesAsync(int productId)
        {
            // Validate productId
            if (productId <= 0)
            {
                throw new ArgumentException("Product ID must be a positive integer.", nameof(productId));
            }

            // Retrieve product sales summary from the repository
            var salesSummary = await _productRepository.GetProductSalesSummaryAsync(productId);

            // Ensure sales summary is not null or empty
            if (salesSummary == null || !salesSummary.Any())
            {
                throw new InvalidOperationException("Failed to retrieve product sales summary.");
            }

            return salesSummary;
        }

        // Method to get all products asynchronously
        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            // Retrieve products from the repository
            var products = await _productRepository.GetProductsAsync();

            // Ensure products are not null or empty
            if (products == null || !products.Any())
            {
                throw new InvalidOperationException("Failed to retrieve products.");
            }

            return products;
        }
    }
}
