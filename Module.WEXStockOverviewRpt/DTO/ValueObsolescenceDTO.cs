﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WEXStockOverviewRpt.DTO
{
    public class ValueObsolescenceDTO
    {
        public int? ProductID { get; set; }
        public int? Obsolete { get; set; }
        public decimal? ValueObsolescence { get; set; }
    }
}
