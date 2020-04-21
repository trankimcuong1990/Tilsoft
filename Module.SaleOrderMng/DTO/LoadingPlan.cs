using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SaleOrderMng.DTO
{
    public class LoadingPlan
    {
        public int? LoadingPlanID { get; set; }

        public string ContainerNo { get; set; }

        public string BLNo { get; set; }

        public string BookingUD { get; set; }

        public List<LoadingPlanDetail> LoadingPlanDetails { get; set; }
        public List<LoadingPlanSparepartDetail> LoadingPlanSparepartDetails { get; set; }
    }
}
