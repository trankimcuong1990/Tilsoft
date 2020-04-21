﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryOrderMng
{
    public class DataSearchContainer
    {
        public List<SaleOrderSearch> SaleOrderSearchs { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }
        public List<DTO.Support.Saler> Salers { get; set; }
        public List<DTO.Support.Factory> Factories { get; set; }
    }
}
