using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrderRpt.DTO
{
    public class ReceiptOverviewDetail
    {
        public int ReceiptID { get; set; }
        public int ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public int ReceiptType { get; set; }
        public int WorkOrderID { get; set; }
        public int CompanyID { get; set; }
    }
}
