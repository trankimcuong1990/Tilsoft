﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt.DTO
{
    public class ActiveCompanyWeeklyHit
    {
        public int CompanyID { get; set; }
        public string WeekStart { get; set; }
        public int WeekIndex { get; set; }
        public int TotalHit { get; set; }
    }
}
