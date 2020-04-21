using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class TransferWorkOrderDetailDTO
    {
        public int TransferWorkOrderDetailID { get; set; }
        public int? TransferWorkOrderID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? TransferQuantity { get; set; }
        public int? PreOrderWorkOrderID { get; set; }

        public string ProductionItemUD { get; set; }
        public decimal? PreOrderWorkOrderQuantity { get; set; }
        public decimal? WorkOrderQuantity { get; set; }
    }
}
