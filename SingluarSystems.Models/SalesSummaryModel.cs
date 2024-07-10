using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingluarSystems.Models
{
    public class SalesSummaryModel
    {
        public decimal TotalSalePrice { get; set; }
        public int TotalSaleQty { get; set; }
        public int DaysToSell { get; set; }
        public decimal AverageSalePricePerUnit { get; set; }
        public decimal MaxSalePrice { get; set; }
        public decimal MinSalePrice { get; set; }
        public string DateRange { get; set; }
    }
}
