using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StockStatusQntRpt.DTO
{
    public class FactoryWarehouseDTO
    {
        public int FactoryWarehouseID { get; set; }
        public string FactoryWarehouseUD { get; set; }
        public string FactoryWarehouseNM { get; set; }
        public int? BranchID { get; set; }
        public int? CompanyID { get; set; }
    }
}
