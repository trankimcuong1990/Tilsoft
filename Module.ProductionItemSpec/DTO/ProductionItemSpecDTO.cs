﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItemSpec.DTO
{
    public class ProductionItemSpecDTO
    {
        public int? ProductionItemSpecID { get; set; }
        public string ProductionItemSpecUD { get; set; }
        public string ProductionItemSpecNM { get; set; }
        public int? ProductionItemSpecTypeID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorNM { get; set; }
    }
}
