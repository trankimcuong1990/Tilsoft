using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseInvoiceMng.DTO
{
    public class PurchaseInvoiceDetailDTO
    {
        public int PurchaseInvoiceDetailID { get; set; }
        public Nullable<int> PurchaseInvoiceID { get; set; }
        public Nullable<int> PurchaseOrderDetailID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string Remark { get; set; }
        public Nullable<int> PurchaseOrderID { get; set; }
        public Nullable<decimal> OrderQnt { get; set; }
        public Nullable<decimal> POPrice { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public Nullable<decimal> TotalReceivedQnt { get; set; }
        public int? FactoryWarehouseID { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public string PurchaseOrderUD { get; set; }
        public int? Type { get; set; }
        public Nullable<decimal> POReceivedQnt { get; set; }
    }
}
