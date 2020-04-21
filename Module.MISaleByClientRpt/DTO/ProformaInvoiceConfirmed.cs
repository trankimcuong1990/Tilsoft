using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MISaleByClientRpt.DTO
{
    public class ProformaInvoiceConfirmed
    {
        public int ClientID { get; set; }
        public string SaleUD { get; set; }
        public string ClientNM { get; set; }
        public string ClientCountryUD { get; set; }
        public Nullable<decimal> PiConfirmedContThisYear { get; set; }
        public Nullable<decimal> PiConfirmedSaleAmountThisYear { get; set; }
        public Nullable<decimal> PiConfirmedDelta5AmountThisYear { get; set; }
        public Nullable<decimal> PiConfirmedDelta5PercentThisYear { get; set; }
        public Nullable<decimal> PiConfirmedPurchasingAmountThisYear { get; set; }
        public Nullable<decimal> CiAmountInUSDLastYear { get; set; }
        public Nullable<decimal> CiContLastYear { get; set; }
        public Nullable<decimal> PiConfirmedContLastYear { get; set; }
        public Nullable<decimal> PiConfirmedSaleAmountLastYear { get; set; }
        public Nullable<decimal> PiConfirmedDelta5AmountLastYear { get; set; }
        public Nullable<decimal> PiConfirmedDelta5PercentLastYear { get; set; }
        public Nullable<decimal> PiConfirmedPurchasingAmountLastYear { get; set; }
    }
}
