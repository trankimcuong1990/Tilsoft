//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductionItem.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductionItemMng_ProductionItem_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductionItemMng_ProductionItem_View()
        {
            this.ProductionItemMng_ProductionItemVender_View = new HashSet<ProductionItemMng_ProductionItemVender_View>();
            this.ProductionItemMng_ProductionItemWarehouse_View = new HashSet<ProductionItemMng_ProductionItemWarehouse_View>();
            this.ProductionItemMng_ProductionItemUnit_View = new HashSet<ProductionItemMng_ProductionItemUnit_View>();
        }
    
        public int ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> InventoryUnit { get; set; }
        public string ValuationMethod { get; set; }
        public Nullable<decimal> ItemCost { get; set; }
        public Nullable<int> RequiredPurchasing { get; set; }
        public Nullable<int> Minimum { get; set; }
        public Nullable<int> Maximum { get; set; }
        public Nullable<int> MRP { get; set; }
        public Nullable<int> ProcurementMethod { get; set; }
        public Nullable<int> LeadTime { get; set; }
        public string PreferVender { get; set; }
        public Nullable<int> PackingUnit { get; set; }
        public Nullable<int> QuantityPerPacking { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> ProductionItemGroupID { get; set; }
        public Nullable<int> ProductionItemMaterialTypeID { get; set; }
        public Nullable<int> ProductionItemSpecID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<int> ProductionItemTypeID { get; set; }
        public string ProductionItemVNNM { get; set; }
        public string UserfulLife { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<decimal> InitiateValue { get; set; }
        public Nullable<decimal> IncreasedAdjustmentValue { get; set; }
        public Nullable<decimal> AccumulatedDepreciation { get; set; }
        public Nullable<decimal> DecreasedAdjustmentValue { get; set; }
        public Nullable<decimal> DepreciationPerMonth { get; set; }
        public Nullable<decimal> NetValue { get; set; }
        public string OverViewStatus { get; set; }
        public string AccountDetermination { get; set; }
        public Nullable<int> AssetClassID { get; set; }
        public Nullable<int> DepreciationTypeID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public Nullable<int> BasedOn { get; set; }
        public Nullable<bool> IsAutoAdd { get; set; }
        public Nullable<bool> isAVTGroup { get; set; }
        public Nullable<int> BreakdownCategoryID { get; set; }
        public Nullable<decimal> Average { get; set; }
        public Nullable<int> OutsourcingCostTypeID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionItemMng_ProductionItemVender_View> ProductionItemMng_ProductionItemVender_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionItemMng_ProductionItemWarehouse_View> ProductionItemMng_ProductionItemWarehouse_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionItemMng_ProductionItemUnit_View> ProductionItemMng_ProductionItemUnit_View { get; set; }
    }
}
