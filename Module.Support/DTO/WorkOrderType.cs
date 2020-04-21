using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class WorkOrderType
    {
        public int WorkOrderTypeID { get; set; }
        public string WorkOrderTypeNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
