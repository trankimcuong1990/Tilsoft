using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class WorkOrderApprovedProductionItem
    {
        public int? WorkOrderID { get; set; }

        public int? ProductionItemID { get; set; }

        public string ProductionItemUD { get; set; }

        public string ProductionItemNM { get; set; }

        public string Unit { get; set; }

        public decimal? Quantity { get; set; }

        public int PrimaryID { get; set; }
    }
}
