using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DTO
{
    public class PurchaseOrderDetailReceivingPlanDto
    {
        public int PurchaseOrderDetailReceivingPlanID { get; set; }
        public int PurchaseOrderDetailID { get; set; }
        public string PlannedETA { get; set; }
        public decimal PlannedReceivingQnt { get; set; }
        public string Remark { get; set; }
    }
}
