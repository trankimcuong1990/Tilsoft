using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItem.DTO
{
    public class ProductionItem
    {
        public int? ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemVNNM { get; set; }
        public string Unit { get; set; }
        public string UnitNM { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? Status { get; set; }
        public int? InventoryUnit { get; set; }
        public string ValuationMethod { get; set; }
        public decimal? ItemCost { get; set; }
        public int? RequiredPurchasing { get; set; }
        public int? Minimum { get; set; }
        public decimal? Average { get; set; }
        public int? Maximum { get; set; }
        public int? MRP { get; set; }  
        public int? ProcurementMethod { get; set; }
        public int? LeadTime { get; set; }
        public string PreferVender { get; set; }
        public int? PackingUnit { get; set; }
        public int? QuantityPerPacking { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string UpdatorName { get; set; }
        public int? CountryID { get; set; }
        public int? ProductionItemGroupID { get; set; }
        public int? ProductionItemMaterialTypeID { get; set; }
        public int? ProductionItemSpecID { get; set; }
        public int? UnitID { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public int? BreakdownCategoryID { get; set; }

        public string UserfulLife { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal InitiateValue { get; set; }
        public decimal AccumulatedDepreciation { get; set; }
        public decimal IncreasedAdjustmentValue { get; set; }
        public decimal DecreasedAdjustmentValue { get; set; }
        public decimal DepreciationPerMonth { get; set; }
        public decimal NetValue { get; set; }
        public string AccountDetermination { get; set; }
        public string OverViewStatus { get; set; }
        public int? AssetClassID { get; set; }
        public int? DepreciationTypeID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public int? BasedOn { get; set; }
        public bool? IsAutoAdd { get; set; }
        public bool? isAVTGroup { get; set; }
        public int? OutsourcingCostTypeID { get; set; }

        // Image
        public bool FileLocation_HasChange { get; set; }
        public string FileLocation_NewFile { get; set; }
        public List<ProductionItemWarehouse> ProductionItemWarehouses { get; set; }
        public List<ProductionItemVender> ProductionItemVenders { get; set; }
        public List<DTO.ProductionItemSubUnitDTO> productionItemSubUnitDTOs { get; set; }

        public int? UserGroupID { get; set; }

        public ProductionItem()
        {
            ProductionItemWarehouses = new List<ProductionItemWarehouse>();
            ProductionItemVenders = new List<ProductionItemVender>();
            productionItemSubUnitDTOs = new List<ProductionItemSubUnitDTO>();
        }

    }
}
