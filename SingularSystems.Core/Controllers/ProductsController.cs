using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SingluarSystems.Abstraction.Interface;
using SingluarSystems.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingularSystems.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("products")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductsAsync()
        {
            try
            {
                var products = await _productService.GetProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("product-sales-summary")]
        public async Task<ActionResult<SalesSummaryModel>> GetProductSalesSummaryAsync([FromQuery] int productId)
        {
            try
            {
                var productSales = await _productService.GetProductSalesAsync(productId);

                var salesSummary = _productService.CalculateSalesSummary(productSales);

                return Ok(salesSummary);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid argument exception in GetProductSalesSummaryAsync.");
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid operation exception in GetProductSalesSummaryAsync.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in GetProductSalesSummaryAsync.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
