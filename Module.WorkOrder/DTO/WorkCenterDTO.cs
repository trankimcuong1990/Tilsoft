﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class WorkCenterDTO
    {
        public int WorkCenterID { get; set; }
        public string WorkCenterUD { get; set; }
        public string WorkCenterNM { get; set; }
        public decimal? OperatingTime { get; set; }
    }
}
