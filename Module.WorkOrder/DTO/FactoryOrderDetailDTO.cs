using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class FactoryOrderDetailDTO
    {
        public int? FactoryOrderDetailID { get; set; }
        public int? ProductID { get; set; }
        public int? FactoryID { get; set; }
        public int? SaleOrderID { get; set; }
        public int? ClientID { get; set; }

        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? OrderQnt { get; set; }
        public string LDS { get; set; }
        public string FactoryUD { get; set; }
        public int? ModelID { get; set; }
        public int? Produced { get; set; }

    }
}
