﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryInvoiceMng.DTO
{
    public class FactoryInvoiceSparepartDetail
    {
        public int FactoryInvoiceSparepartDetailID { get; set; }
        public int? FactoryOrderSparepartDetailID { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? SubTotal { get; set; }
        public string Remark { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ClientUD { get; set; }
    }
}
