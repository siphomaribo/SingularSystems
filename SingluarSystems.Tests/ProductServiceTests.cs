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
                new ProductSaleModel { SaleId = 1, ProductId = 1, SalePrice = 15.33, SaleQty = 10, SaleDate = DateTime.Parse("2023-01-01") }
            };

            _productRepositoryMock.Setup(repo => repo.GetProductSalesSummaryAsync(1)).ReturnsAsync(salesSummary);

            // Act
            var result = await _productService.GetProductSalesSummaryAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().SalePrice, Is.EqualTo(15.33).Within(0.001));
        }

        [Test]
        public void GetProductSalesSummaryAsync_InvalidProductId_ThrowsArgumentException()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _productService.GetProductSalesSummaryAsync(0));
            Assert.That(ex.Message, Is.EqualTo("Product ID must be a positive integer. (Parameter 'productId')"));
        }

        [Test]
        public void GetProductSalesSummaryAsync_ThrowsExceptionWhenRepositoryFails()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.GetProductSalesSummaryAsync(1)).ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _productService.GetProductSalesSummaryAsync(1));
            Assert.That(ex.Message, Is.EqualTo("Repository error"));
        }

        [Test]
        public void GetProductSalesSummaryAsync_ReturnsNullSalesSummary_ThrowsInvalidOperationException()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.GetProductSalesSummaryAsync(1)).ReturnsAsync((IEnumerable<ProductSaleModel>)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _productService.GetProductSalesSummaryAsync(1));
            Assert.That(ex.Message, Is.EqualTo("Failed to retrieve product sales summary."));
        }
    }
}
