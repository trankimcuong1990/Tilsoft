using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySaleInvoice.DTO
{
    public class FactorySaleInvoiceDetailDTO
    {
        public int FactorySaleInvoiceDetailID { get; set; }
        public int? FactorySaleInvoiceID { get; set; }
        public int? FactorySaleOrderDetailID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Remark { get; set; }
        public int? FactorySaleOrderID { get; set; }
        public decimal? OrderQnt { get; set; }
        public decimal? FSOPrice { get; set; }
        public string FactorySaleOrderUD { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public decimal? RemainQnt { get; set; }
    }
}
