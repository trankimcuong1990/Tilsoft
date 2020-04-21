using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class WorkOrderReportDTO
    {
        public int WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }      
        public List<ProductionItemDTO> ProductionItemDTOs { get; set; }
    }
}
