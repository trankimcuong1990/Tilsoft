using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkCenterMng.DTO
{
    public class WorkCenterDetailDTO
    {
        public int WorkCenterDetailID { get; set; }

        public int? WorkCenterID { get; set; }

        public int? BranchID { get; set; }

        public int? FactoryWarehouseID { get; set; }

        public bool? IsDefault { get; set; }

        public int? CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public string BranchUD { get; set; }

        public string BranchNM { get; set; }

        public string FactoryWarehouseUD { get; set; }

        public string FactoryWarehouseNM { get; set; }

        public string CreatorName { get; set; }

        public string UpdatorName { get; set; }
    }
}
