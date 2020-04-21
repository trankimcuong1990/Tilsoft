using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SaleOrderMng
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