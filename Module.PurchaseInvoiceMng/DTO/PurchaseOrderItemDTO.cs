using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseInvoiceMng.DTO
{
    public class PurchaseOrderItemDTO
    {
        public int PurchaseOrderDetailID { get; set; }
        public Nullable<int> PurchaseOrderID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<decimal> OrderQnt { get; set; }
        public Nullable<decimal> POPrice { get; set; }
        public string Remark { get; set; }
        public string PurchaseOrderUD { get; set; }
        public string PurchaseOrderDate { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public Nullable<int> ProductionItemTypeID { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public Nullable<decimal> TotalReceivedQnt { get; set; }
        public Nullable<int> ReceivingNoteID { get; set; }
        public string ReceivingNoteUD { get; set; }
        public Nullable<decimal> ReceivedQNT { get; set; }
        public Nullable<decimal> InvoiceQNT { get; set; }
        public Nullable<decimal> RemainQNT { get; set; }
        public Nullable<int> Type { get; set; }
        public decimal? POReceivedQnt { get; set; }
        public string ReceivingNoteDate { get; set; }
    }
}
