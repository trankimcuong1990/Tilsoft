using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrderRpt.DTO
{
    public class WorkOrder
    {
        public int WorkOrderID { get; set; }
        public int CompanyID { get; set; }
        public string WorkOrderUD { get; set; }
        public string ClientUD { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
