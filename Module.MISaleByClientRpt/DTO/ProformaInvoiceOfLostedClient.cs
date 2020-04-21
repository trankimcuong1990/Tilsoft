using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MISaleByClientRpt.DTO
{
    public class ProformaInvoiceOfLostedClient
    {
        public int ClientID { get; set; }
        public string SaleUD { get; set; }
        public string ClientNM { get; set; }
        public string ClientCountryUD { get; set; }
        public Nullable<decimal> PICont { get; set; }
        public Nullable<decimal> PIAmountInUSD { get; set; }
        public Nullable<decimal> DeltaAfterAllInEUR { get; set; }
        public Nullable<decimal> DeltaAfterAllPercent { get; set; }
        public Nullable<decimal> PurchasingAmountInEUR { get; set; }
    }
}
