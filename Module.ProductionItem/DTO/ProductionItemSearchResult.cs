using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItem.DTO
{
    public class ProductionItemSearchResult
    {
        public int? ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemVNNM { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? Status { get; set; }
        public int? Inventory { get; set; }
        public string ValuationMethod { get; set; }
        public decimal? ItemCost { get; set; }
        public int? RequiredPurchasing { get; set; }
        public int? Minimum { get; set; }
        public int? Maximum { get; set; }
        public int? MRP { get; set; }
        public int? ProcurementMethod { get; set; }
        public int? LeadTime { get; set; }
        public string PreferVender { get; set; }
        public string PackingUnit { get; set; }
        public int? QuantityPerPacking { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string UpdatorName { get; set; }
        public int? UnitID { get; set; }
        public string UnitNM { get; set; }
        public int? ProductionItemTypeID { get; set; }

        public int? ProductionItemGroupID { get; set; }
        public int? ProductionItemMaterialTypeID { get; set; }
        public int? ProductionItemSpecID { get; set; }
        public string ProductionItemGroupNM { get; set; }
        public string ProductionItemMaterialTypeNM { get; set; }
        public string ProductionItemSpecNM { get; set; }
        public string ProductionItemTypeNM { get; set; }

        // Factory Warehouse
        public int? DefaultWarehouseID { get; set; }
        public string FactoryWarehouseNM { get; set; }
        public int? CompanyID { get; set; }
        public bool? isAVTGroup { get; set; }

        public int? FactoryWarehouseID { get; set; }
    }
}
