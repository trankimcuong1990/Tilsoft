using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkCenterMng.DTO
{
    public class WorkCenterDetailSearchResultDTO
    {
        public int? WorkCenterID { get; set; }

        public int? FactoryWarehouseID { get; set; }

        public string FactoryWarehouseUD { get; set; }

        public string FactoryWarehouseNM { get; set; }

        public int PrimaryID { get; set; }
    }
}
