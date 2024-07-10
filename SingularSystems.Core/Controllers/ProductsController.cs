using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingluarSystems.Abstraction.Interface;
using SingluarSystems.Models;

namespace SingularSystems.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
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
                // Log exception and return appropriate error response
                return StatusCode(500, $"Internal server error: {ex.Message}");
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
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log exception and return appropriate error response
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
