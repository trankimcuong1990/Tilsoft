﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SaleOrderMng.DTO
{
    public class LoadingPlan2
    {
        public int? LoadingPlanID { get; set; }

        public string ContainerNo { get; set; }

        public string BLNo { get; set; }

        public string BookingUD { get; set; }

        public List<LoadingPlanDetail2> LoadingPlanDetail2s { get; set; }
        public List<LoadingPlanSparepartDetail2> LoadingPlanSparepartDetail2s { get; set; }
    }
}
