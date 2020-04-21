using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MIEurofarPriceOverviewRpt.DTO
{
    public class SupplierData
    {
        public object PrimaryID { get; set; }
        public string Season { get; set; }
        public string FactoryUD { get; set; }
        public decimal? OrderQnt40HC { get; set; }
        public decimal? FOBCostUSD { get; set; }
        public decimal? GrossMarginAfterCommissionUSD { get; set; }
        public decimal? ShippedQnt40HC { get; set; }
        public decimal? ToBeShippedQnt40HC { get; set; }
        public string FactoryName { get; set; }
    }
}
