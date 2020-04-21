using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class WorkOrderDetailDTO
    {
        public int? WorkOrderDetailID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? ProductID { get; set; }
        public int? Quantity { get; set; }

        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? OrderQnt { get; set; }
        public string LDS { get; set; }
        public string FactoryUD { get; set; }
        public int? ModelID { get; set; }



    }
}
