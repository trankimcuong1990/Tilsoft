﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class SalePriceTableLastSeason
    {
        public int OfferSeasonDetailID { get; set; }
        public decimal? SalePrice { get; set; }
        public string Currency { get; set; }
    }
}
