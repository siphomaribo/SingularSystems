using SingluarSystems.Models;

namespace SingluarSystems.Abstraction.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetProductsAsync();
        Task<IEnumerable<ProductSaleModel>> GetProductSalesAsync(int productId);
        SalesSummaryModel CalculateSalesSummary(IEnumerable<ProductSaleModel> productSales);
    }
}
