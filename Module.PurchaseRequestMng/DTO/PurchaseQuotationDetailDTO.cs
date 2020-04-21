using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseRequestMng.DTO
{
    public class PurchaseQuotationDetailDTO
    {
        public int PurchaseQuotationDetailID { get; set; }
        public int? PurchaseQuotationID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? OrderQnt { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool? IsDefault { get; set; }
    }
}
