using SingluarSystems.ExternalServices.Interfaces;
using SingluarSystems.Models;

namespace SingluarSystems.ExternalServices.HttpProductAPI
{
    public class SalesAPI : IProductRepository
    {
        public Task<IEnumerable<ProductSaleModel>> GetProductSalesSummaryAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
