using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrderRpt.DTO
{
    public class FactoryWarehouse
    {
        public int WorkOrderID { get; set; }
        public int CompanyID { get; set; }
        public int OPSequenceDetailID { get; set; }
        public int FactoryWarehouseID { get; set; }
        public string FactoryWarehouseUD { get; set; }
        public string FactoryWarehouseNM { get; set; }
    }
}
