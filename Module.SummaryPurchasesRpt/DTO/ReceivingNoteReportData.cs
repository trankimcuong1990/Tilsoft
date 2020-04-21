using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SummaryPurchasesRpt.DTO
{
    public class ReceivingNoteReportData
    {
        public int? ReceivingNoteID { get; set; }
        public string ReceivingNoteUD { get; set; }
        public int? DeliveryNoteID { get; set; }
        public string DeliveryNoteUD { get; set; }
        public string ReceivingNoteDateForHTML { get; set; }
        public string ReceivingNoteDateForExcel { get; set; }
        public string PurchasingOrderNo { get; set; }
        public int? CompanyID { get; set; }
        public string DeliverName { get; set; }
        public string DeliverAddress { get; set; }
        public int ReceivingNoteDetailID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? PurchaseValue { get; set; }
        public decimal? QuantityReturn { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public decimal? ValueReturn { get; set; }
        public int? PurchaseOrderID { get; set; }
        public decimal? VAT { get; set; }

        public string BranchUD { get; set; }
        public string BranchNM { get; set; }

        public int? DeliveryNoteDetailID { get; set; }
        public long KeyID { get; set; }
        public int? DetailType { get; set; }
    }
}
