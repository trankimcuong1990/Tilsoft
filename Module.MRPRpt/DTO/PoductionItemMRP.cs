using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MRPRpt.DTO
{
    public class PoductionItemMRP
    {
        public int? ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public int? MinQnt { get; set; }
        public int? MaxQnt { get; set; }
        public int? LeadTime { get; set; }
        public decimal? ProjectOnHand { get; set; }

        public List<DTO.PurchaseOrderDetailMRP> purchaseOrderDetailMRPs { get; set; }
        public List<DTO.PurchaseRequestDetailMRP> purchaseRequestDetailMRPs { get; set; }
    }
}
