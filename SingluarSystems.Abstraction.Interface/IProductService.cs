using SingluarSystems.Models;

namespace SingluarSystems.Abstraction.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetProductsAsync();
        Task<IEnumerable<ProductSaleModel>> GetProductSalesSummaryAsync(int productId);
    }
}
