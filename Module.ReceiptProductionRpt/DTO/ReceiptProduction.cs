using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceiptProductionRpt.DTO
{
    public class ReceiptProduction
    {
        public int PrimaryID { get; set; }
        public int BOMID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? WorkCenterID { get; set; }

        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string WorkOrderUD { get; set; }

        public List<ReceiptProductionDetail> ReceiptProductionDetails { get; set; }
    }
}
