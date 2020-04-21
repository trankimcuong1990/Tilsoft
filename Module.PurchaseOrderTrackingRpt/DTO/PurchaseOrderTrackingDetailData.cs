using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderTrackingRpt.DTO
{
    public class PurchaseOrderTrackingDetailData
    {
        public int PrimaryID { get; set; }
        public int? PurchaseOrderID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? ClientID { get; set; }

        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public string WorkOrderUD { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string ClientUD { get; set; }
        public string PurchaseOrderETA { get; set; }
        public string PurchaseOrderDetailETA { get; set; }

        public decimal? OrderQnt { get; set; }
        public decimal? ReceiveQnt { get; set; }
        public decimal? BalanceQnt { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
