using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class HistoryTransferPreOrderToWorkOrderDTO
    {
        public int TransferWorkOrderID { get; set; }
        public string TransferWorkOrderUD { get; set; }
        public string TransferWorkOrderDate { get; set; }
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public int? PreOrderWorkOrderID { get; set; }
        public string CreatorName { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
    }
}
