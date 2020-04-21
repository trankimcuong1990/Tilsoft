using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MISaleByClientRpt.DTO
{
    public class Expectation
    {
        public int ClientID { get; set; }
        public string SaleUD { get; set; }
        public string ClientNM { get; set; }
        public string ClientCountryUD { get; set; }
        public Nullable<decimal> ExpectedContThisYear { get; set; }
        public Nullable<decimal> ExpectedAmountInUSDThisYear { get; set; }
        public Nullable<decimal> CiContLastYear { get; set; }
        public Nullable<decimal> CiAmountInUSDLastYear { get; set; }
        public Nullable<decimal> PiConfirmedContLastYear { get; set; }
        public Nullable<decimal> PiConfirmedSaleAmountLastYear { get; set; }
        public Nullable<decimal> PiConfirmedDelta5AmountLastYear { get; set; }
        public Nullable<decimal> PiConfirmedDelta5PercentLastYear { get; set; }
        public Nullable<decimal> PiConfirmedPurchasingAmountLastYear { get; set; }
    }
}
