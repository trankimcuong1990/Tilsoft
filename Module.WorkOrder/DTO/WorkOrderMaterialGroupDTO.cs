using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class WorkOrderMaterialGroupDTO
    {
        public long PrimaryID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? ProductionItemGroupID { get; set; }
        public string ProductionItemGroupNM { get; set; }
    }
}
