using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarPurchasingPriceMng.DTO
{
    public class ImportQuotationDetailDTO
    {
        public int QuotationDetailID { get; set; }
        public string Season { get; set; }
        public string NewTargetComment { get; set; }
        public decimal TargetPrice { get; set; }
    }
}
