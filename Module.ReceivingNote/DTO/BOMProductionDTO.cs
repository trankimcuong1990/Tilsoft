using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceivingNote.DTO
{
    public class BOMProductionDTO
    {
        public int PrimaryID { get; set; }
        public int? BOMID { get; set; }
        public int? WorkOrderID { get; set; }
        public decimal? BOMQnt { get; set; }
        public decimal? BOMQntTotal { get; set; }
        public string WorkOrderUD { get; set; }
        public int? ParentProductionItemID { get; set; }
        public string ParentProductionItemUD { get; set; }
        public string ParentProductionItemNM { get; set; }
        public int? ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
    }
}
