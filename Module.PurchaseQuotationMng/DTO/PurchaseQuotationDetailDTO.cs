using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseQuotationMng.DTO
{
   public class PurchaseQuotationDetailDTO
    {
        public int PurchaseQuotationDetailID { get; set; }

        public int? PurchaseQuotationID { get; set; }

        public int? ProductionItemID { get; set; }

        public string ProductionItemUD { get; set; }

        public string ProductionItemNM { get; set; }

        public string UnitNM { get; set; }

        public decimal? OrderQnt { get; set; }

        public decimal? UnitPrice { get; set; }

        public string Remark { get; set; }

        public bool? IsDefault { get; set; }

        public decimal? FrameWeight { get; set; }

        public int? ProductionItemTypeID { get; set; }

        public bool? IsHasWeight { get; set; }
    }
}
