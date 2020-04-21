using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryWarehouse.DTO
{
    public class FactoryWarehouseSearch
    {
        public int? FactoryWarehouseID { get; set; }
        public string FactoryWarehouseUD { get; set; }
        public string FactoryWarehouseNM { get; set; }
        public string Description { get; set; }
        public int? CompanyID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CompanyNM { get; set; }
        public string UpdatorNM { get; set; }

        public int? BranchID { get; set; }

        public string BranchUD { get; set; }

        public string BranchNM { get; set; }

        public string CompanyUD { get; set; }

        public string ShortName { get; set; }

        public int? ResponsibleBy { get; set; }
        public string ResponsibleNM { get; set; }
    }
}
