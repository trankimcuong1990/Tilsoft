//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductionPriceMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductionPriceMng_ProductionPriceSearch_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductionPriceMng_ProductionPriceSearch_View()
        {
            this.ProductionPriceMng_ProductionPriceDetailSearch_View = new HashSet<ProductionPriceMng_ProductionPriceDetailSearch_View>();
        }
    
        public int ProductionPriceID { get; set; }
        public Nullable<int> ProductionItemTypeID { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public bool IsLocked { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionPriceMng_ProductionPriceDetailSearch_View> ProductionPriceMng_ProductionPriceDetailSearch_View { get; set; }
    }
}
