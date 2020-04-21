using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class ProductionItemDTO
    {
        public int ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public int WorkCenterID { get; set; }
        public string WorkCenterUD { get; set; }
        public string WorkCenterNM { get; set; }
        public Nullable<int> Quantity { get; set; }
        public int WorkOrderID { get; set; }
        public long PrimaryID { get; set; }      
    }
}
