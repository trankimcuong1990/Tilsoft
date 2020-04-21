using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class TransferWorkOrderDTO
    {
        public int TransferWorkOrderID { get; set; }
        public string TransferWorkOrderUD { get; set; }
        public string TransferWorkOrderDate { get; set; }
        public int WorkOrderID { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string LDS { get; set; }
        public int? ProductionQuantity { get; set; }
        public string WorkOrderUD { get; set; }

        public List<TransferWorkOrderDetailDTO> TransferWorkOrderDetails { get; set; }

        public TransferWorkOrderDTO()
        {
            TransferWorkOrderDetails = new List<TransferWorkOrderDetailDTO>();
        }
    }
}
