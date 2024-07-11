using SingluarSystems.Abstraction.Interface;
using SingluarSystems.ExternalServices.Interfaces;
using SingluarSystems.Models;

namespace SingluarSystems.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public SalesSummaryModel CalculateSalesSummary(IEnumerable<ProductSaleModel> productSales)
        {
            if (productSales == null)
            {
                throw new ArgumentNullException(nameof(productSales));
            }

            decimal totalSalePrice = productSales.Sum(s => s.SalePrice * s.SaleQty);
            int totalSaleQty = productSales.Sum(s => s.SaleQty);

            DateTime earliestSaleDate = productSales.Min(s => s.SaleDate.Value);
            DateTime latestSaleDate = productSales.Max(s => s.SaleDate.Value);
            int daysToSell = (int)(latestSaleDate - earliestSaleDate).TotalDays + 1;

            decimal averageSalePricePerUnit = totalSalePrice / totalSaleQty;

            decimal maxSalePrice = productSales.Max(s => s.SalePrice);
            decimal minSalePrice = productSales.Min(s => s.SalePrice);

            string dateRange = $"{earliestSaleDate.ToShortDateString()} - {latestSaleDate.ToShortDateString()}";

            var summary = new SalesSummaryModel();

            summary.SetSummary(totalSalePrice, totalSaleQty, daysToSell, averageSalePricePerUnit, maxSalePrice, minSalePrice, dateRange);

            return summary;
        }

        public async Task<IEnumerable<ProductSaleModel>> GetProductSalesAsync(int productId)
        {
            //We can add extra validation as we wish. e.g how check for user roles and so forth

            if (productId <= 0)
            {
                throw new ArgumentException("Product ID must be a positive integer.", nameof(productId));
            }

            var salesSummary = await _productRepository.GetProductSalesSummaryAsync(productId);

            // Additional Business Rule Validation: Ensure sales summary is not null or empty
            if (salesSummary == null)
            {
                throw new InvalidOperationException("Failed to retrieve product sales summary.");
            }

            return salesSummary;

          
        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            //We can add extra validation as we wish. e.g how check for user roles and so forth

            var products = await _productRepository.GetProductsAsync();

            if (products == null)
            {
                throw new InvalidOperationException("Failed to retrieve products.");
            }

            return products;
        }
    }
}
