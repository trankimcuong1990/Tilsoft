using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySpecificationMng.DTO
{
    public class FactoryOrderDetail
    {
        public int? FactoryOrderDetailID { get; set; }
        public string FactoryOrderUD { get; set; }
        public int? OrderQnt { get; set; }
        public string LDS { get; set; }
        public int? FactoryOrderID { get; set; }
        public int? SaleOrderID { get; set; }
    }
}
