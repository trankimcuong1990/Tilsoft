﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingCostMng.DTO
{
    public class SearchFormData
    {
        public List<OutsourcingCostSearchDTO> Data { get; set; }
        public List<WorkCenterDTO> WorkCenters { get; set; }
    }
}
