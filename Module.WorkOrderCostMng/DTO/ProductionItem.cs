﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrderCostMng.DTO
{
    public class ProductionItem
    {
        public int ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public string UnitNM { get; set; }
        public int? UnitID { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public int? DefaultFactoryWarehouseID { get; set; }        
        public int? Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public decimal? WastagePercent { get; set; }

        public List<ProductionItemUnit> ProductionItemUnits { get; set; }
    }

    public class ProductionItemUnit
    {
        public int? ProductionItemID { get; set; }
        public int? UnitID { get; set; }
        public decimal? ConversionFactor { get; set; }
        public string UnitNM { get; set; }
    }
}
