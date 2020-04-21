using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseInvoiceMng.DTO
{
    public class TotalAmountDTO
    {
        public decimal? totalAmountVND { get; set; }
        public decimal? totalAMountUSD { get; set; }
        public decimal? totalVATAmount { get; set; }
        public decimal? totalAllAmountVND { get; set; }
    }
}
