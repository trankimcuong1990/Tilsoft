using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySaleInvoice.DTO
{
    public class FactorySaleOrderItemDTO
    {
        public string FactorySaleOrderUD { get; set; }
        public string DocumentDate { get; set; }
        public int? ProductionItemID { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public decimal? OrderQnt { get; set; }
        public decimal InvoiceQnt { get; set; }
        public decimal? FSOPrice { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string ProductionItemUD { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public int FactorySaleOrderDetailID { get; set; }
        public int? FactorySaleOrderID { get; set; }
        public decimal? RemainQnt { get; set; }
    }
}
