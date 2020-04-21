using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseRequestMng.DTO
{
    public class PurchaseRequestWorkOrderDetailDTO
    {
        public int? PurchaseRequestWorkOrderDetailID { get; set; }
        public int? PurchaseRequestDetailID { get; set; }
        public int? WorkOrderID { get; set; }
        public decimal? RequestedQnt { get; set; }
        public string Remark { get; set; }
        public string WorkOrderUD { get; set; }

    }
}
