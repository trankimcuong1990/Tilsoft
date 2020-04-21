using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class WorkOrderListPreOrderDTO
    {
        public int WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string ArticleCode { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public int? Quantity { get; set; }
        public int? WorkOrderTypeID { get; set; }
        public string WorkOrderTypeNM { get; set; }
        public int? PreOrderWorkOrderID { get; set; }
    }
}
