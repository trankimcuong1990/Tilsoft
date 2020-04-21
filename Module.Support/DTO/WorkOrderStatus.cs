using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class WorkOrderStatus
    {
        public int  WorkOrderStatusID { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
