using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionStatisticsMng.DTO
{
    public class ProductionStatisticsSearchDTO
    {
        public int ProductionStatisticsID { get; set; }
        public int? WorkCenterID { get; set; }
        public string ProducedDate { get; set; }
        public int? WorkerQnt { get; set; }
        public string Remark { get; set; }
        public int? CompanyID { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public string WorkCenterNM { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
    }
}
