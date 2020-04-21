﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class CalculationPriceMangementCost
    {
        public long KeyID { get; set; }
        public int? BreakdownCategoryOptionID { get; set; }
        public int? BreakdownID { get; set; }
        public int? BreakdownCategoryID { get; set; }
        public int? CompanyID { get; set; }
        public decimal? QuantityPercent { get; set; }
        public bool? IsDefaultOption { get; set; }
    }
}
