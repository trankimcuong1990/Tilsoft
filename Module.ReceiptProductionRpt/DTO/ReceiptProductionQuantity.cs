using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceiptProductionRpt.DTO
{
    public class ReceiptProductionQuantity
    {
        public int PrimaryID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? WorkCenterID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? ProductionTeamID { get; set; }
        public string ProductionTeamNM { get; set; }
        public decimal? TotalQnt { get; set; }

        public int? BOMID { get; set; }
    }
}
