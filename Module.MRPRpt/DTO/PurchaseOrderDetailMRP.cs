using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MRPRpt.DTO
{
   public class PurchaseOrderDetailMRP
    {
        public int? ProductionItemID { get; set; }
        public string PlannedETA { get; set; }
        public decimal? PlannedReceivingQnt { get; set; }
    }

}
