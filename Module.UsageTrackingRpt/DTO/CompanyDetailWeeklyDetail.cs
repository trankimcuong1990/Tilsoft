﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt.DTO
{
    public class CompanyDetailWeeklyDetail
    {
        public string ModuleNM { get; set; }
        public int YearIndex { get; set; }
        public int WeekIndex { get; set; }
        public int TotalHit { get; set; }
    }
}
