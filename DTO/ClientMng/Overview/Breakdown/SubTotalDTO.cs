using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.Breakdown
{
    public class SubTotalDTO
    {
        public int? TotalOrderQnt { get; set; }
        public decimal? TotalOrderQnt40HC { get; set; }
        public decimal? SaleAmountEUR { get; set; }
        public decimal? SaleAmountUSD { get; set; }
        public decimal? TotalTransport { get; set; }
        public decimal? FOBAmountUSD { get; set; }
        public decimal? MarginBeforeComm { get; set; }
        public decimal? MarginBeforeCommTarget { get; set; }
        public decimal? CommissionUSD { get; set; }
        public decimal? MarginAfterComm { get; set; }
        public decimal? MarginAfterCommTarget { get; set; }
        public decimal? FOBPurchasingUSD { get; set; }
    }
}
