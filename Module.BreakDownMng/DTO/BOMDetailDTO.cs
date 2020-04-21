using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class BOMDetailDTO
    {
        public int ProductionItemID { get; set; }
        public int GroupBOMStandardID { get; set; }
        public int WorkCenterID { get; set; }
        public int ProductionItemGroupID { get; set; }
        public int UnitID { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? WastedPercent { get; set; }
        public decimal? QuantityPercent { get; set; }

        public string ProductionItemUD { get; set; }
        public string WorkCenterUD { get; set; }
        public string GroupUD { get; set; }
    }
}
