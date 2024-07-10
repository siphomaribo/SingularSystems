using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingluarSystems.Models
{
    public class ProductSaleModel
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public double SalePrice { get; set; }
        public int SaleQty { get; set; }
        public DateTime? SaleDate { get; set; }
    }

}
