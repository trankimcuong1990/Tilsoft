﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2.DTO
{
    public class FactoryMonthlyCapacity
    {
        public int? FactoryID { get; set; }
        public string Season { get; set; }
        public int? MonthlyOrder { get; set; }
        public decimal? TotalOrderQnt40HC { get; set; }
    }
}
