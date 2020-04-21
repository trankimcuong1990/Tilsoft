using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ShowroomRpt.DTO
{
    public class ProductionItemWithDescription
    {
        public int PrimaryID { get; set; }

        public int? ProductionItemID { get; set; }

        public int? FactoryWarehouseID { get; set; }

        public int? CompanyID { get; set; }

        public string Description { get; set; }
    }
}
