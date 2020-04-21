﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.Models.ReportDTO
{
    public class WarehouseTransferDetailPrintoutDTO
    {
        public string ProductionItemNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string UnitNM { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public string ClientID { get; set; }
        public string WorkOrderUD { get; set; }
    }
}