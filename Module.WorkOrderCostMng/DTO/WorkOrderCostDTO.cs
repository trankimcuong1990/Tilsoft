using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrderCostMng.DTO
{
    public class WorkOrderCostDTO
    {
        public int WorkOrderCostID { get; set; }
        public int WorkOrderID { get; set; }
        public int ProductionItemID { get; set; }
        public string productionItemUD { get; set; }
        public string productionItemNM { get; set; }
        public string unitID { get; set; }
        public string unitNM { get; set; }
        public decimal Qty { get; set; }
        public decimal CostPrice { get; set; }
    }
}
