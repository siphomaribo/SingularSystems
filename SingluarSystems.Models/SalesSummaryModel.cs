using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingluarSystems.Models
{
    public class SalesSummaryModel
    {
        public decimal TotalSalePrice { get; private set; }
        public int TotalSaleQty { get; private set; }
        public int DaysToSell { get; private set; }
        public decimal AverageSalePricePerUnit { get; private set; }
        public decimal MaxSalePrice { get; private set; }
        public decimal MinSalePrice { get; private set; }
        public string? DateRange { get; private set; }

        public void SetSummary(decimal totalSalePrice, int totalSaleQty, int daysToSell, decimal averageSalePricePerUnit, decimal maxSalePrice, decimal minSalePrice, string dateRange)
        {
            TotalSalePrice = totalSalePrice;
            TotalSaleQty = totalSaleQty;
            DaysToSell = daysToSell;
            AverageSalePricePerUnit = averageSalePricePerUnit;
            MaxSalePrice = maxSalePrice;
            MinSalePrice = minSalePrice;
            DateRange = dateRange;
        }
    }

}
