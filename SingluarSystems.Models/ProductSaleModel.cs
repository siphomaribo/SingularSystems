using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingluarSystems.Models
{
    public class ProductSaleModel
    {
        public int SaleId { get; private set; }
        public int ProductId { get; private set; }
        public decimal SalePrice { get; private set; }
        public int SaleQty { get; private set; }
        public DateTime? SaleDate { get; private set; }

        public ProductSaleModel(int saleId, int productId, decimal salePrice, int saleQty, DateTime? saleDate)
        {
            SaleId = saleId;
            ProductId = productId;
            SalePrice = salePrice;
            SaleQty = saleQty;
            SaleDate = saleDate;
        }
    }


}
