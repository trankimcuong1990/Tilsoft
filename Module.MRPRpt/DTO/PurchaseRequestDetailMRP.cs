using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MRPRpt.DTO
{
    public class PurchaseRequestDetailMRP
    {
        public int? ProductionItemID { get; set; }
        public string ETA { get; set; }
        public decimal? RequestQnt { get; set; }
    }
}
