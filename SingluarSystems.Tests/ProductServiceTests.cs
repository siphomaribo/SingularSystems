using Moq;
using NUnit.Framework;
using SingluarSystems.Models;
using SingluarSystems.Services;
using SingluarSystems.ExternalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SingluarSystems.Abstraction.Interface;

namespace SingluarSystems.Tests.Services
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private IProductService _productService;

        [SetUp]
        public void SetUp()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Test]
        public async Task GetProductsAsync_ReturnsProducts()
        {
            // Arrange
            var products = new List<ProductModel>
            {
                new ProductModel { Id = 1, Description = "Apples", SalePrice = 15.33m, Category = "Fruit", Image = "https://images.pexels.com/photos/10256309/pexels-photo-10256309.jpeg?auto=compress&cs=tinysrgb&h=350" },
                new ProductModel { Id = 2, Description = "Bananas", SalePrice = 10.00m, Category = "Fruit", Image = "https://images.pexels.com/photos/10256309/pexels-photo-10256309.jpeg?auto=compress&cs=tinysrgb&h=350" }
            };

            _productRepositoryMock.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetProductsAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Description, Is.EqualTo("Apples"));
        }

        [Test]
        public async Task GetProductsAsync_ThrowsExceptionWhenRepositoryFails()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.GetProductsAsync()).ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            Exception ex = null;
            try
            {
                await _productService.GetProductsAsync();
            }
            catch (Exception e)
            {
                ex = e;
            }

            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message, Is.EqualTo("Repository error"));
        }

        [Test]
        public async Task GetProductSalesSummaryAsync_ValidProductId_ReturnsSalesSummary()
        {
            // Arrange
            var salesSummary = new List<ProductSaleModel>
            {
                new ProductSaleModel (  1,1,  15.33m,  10, DateTime.Parse("2023-01-01") )
            };

            _productRepositoryMock.Setup(repo => repo.GetProductSalesSummaryAsync(1)).ReturnsAsync(salesSummary);

            // Act
            var result = await _productService.GetProductSalesAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().SalePrice, Is.EqualTo(15.33).Within(0.001));
        }

        [Test]
        public void GetProductSalesSummaryAsync_InvalidProductId_ThrowsArgumentException()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _productService.GetProductSalesAsync(0));
            Assert.That(ex.Message, Is.EqualTo("Product ID must be a positive integer. (Parameter 'productId')"));
        }

        [Test]
        public void GetProductSalesSummaryAsync_ThrowsExceptionWhenRepositoryFails()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.GetProductSalesSummaryAsync(1)).ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _productService.GetProductSalesAsync(1));
            Assert.That(ex.Message, Is.EqualTo("Repository error"));
        }

        [Test]
        public void GetProductSalesSummaryAsync_ReturnsNullSalesSummary_ThrowsInvalidOperationException()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.GetProductSalesSummaryAsync(1)).ReturnsAsync((IEnumerable<ProductSaleModel>)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _productService.GetProductSalesAsync(1));
            Assert.That(ex.Message, Is.EqualTo("Failed to retrieve product sales summary."));
        }

        [Test]
        public void CalculateSalesSummary_ValidInput_ReturnsCorrectSummary()
        {
            // Arrange
            var productSales = new List<ProductSaleModel>
            {
                new ProductSaleModel ( 365,1, 10.5m,  100, DateTime.Parse("2024-07-01") ),
                new ProductSaleModel ( 366,1,  12.75m, 150, DateTime.Parse("2024-07-02") )
            };

            // Act
            var result = _productService.CalculateSalesSummary(productSales);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.TotalSalePrice, Is.EqualTo(10.5m * 100 + 12.75m * 150));
            Assert.That(result.TotalSaleQty, Is.EqualTo(100 + 150));
            Assert.That(result.DaysToSell, Is.EqualTo(2)); // Assuming sales are within 2 days
            Assert.That(result.AverageSalePricePerUnit, Is.EqualTo((10.5m * 100 + 12.75m * 150) / (100 + 150)));
            Assert.That(result.MaxSalePrice, Is.EqualTo(12.75m));
            Assert.That(result.MinSalePrice, Is.EqualTo(10.5m));
            Assert.That(result.DateRange, Is.EqualTo("2024/07/01 - 2024/07/02"));
        }

        [Test]
        public void CalculateSalesSummary_NullInput_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _productService.CalculateSalesSummary(null));
        }
    }
}
