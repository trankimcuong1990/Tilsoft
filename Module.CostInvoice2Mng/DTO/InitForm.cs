﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CostInvoice2Mng.DTO
{
    public class InitForm
    {
        public List<Support.DTO.Season> Seasons { get; set; }
        public List<CostInvoice2Type> CostInvoice2Types { get; set; }
    }
}
