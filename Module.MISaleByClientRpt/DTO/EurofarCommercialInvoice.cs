using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MISaleByClientRpt.DTO
{
    public class EurofarCommercialInvoice
    {
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string SaleUD { get; set; }
        public Nullable<decimal> CiContThisYear { get; set; }
        public Nullable<decimal> CiContLastYear { get; set; }
        public Nullable<decimal> CiContLastLastYear { get; set; }
        public Nullable<decimal> CiAmountInUSDThisYear { get; set; }
        public Nullable<decimal> CiAmountInUSDLastYear { get; set; }
        public Nullable<decimal> CiAmountInUSDLastLastYear { get; set; }

    }
}
