using SingluarSystems.Models;

namespace SingluarSystems.ExternalServices.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetProductsAsync();
        Task<IEnumerable<ProductSaleModel>> GetProductSalesSummaryAsync(int productId);
    }
}
