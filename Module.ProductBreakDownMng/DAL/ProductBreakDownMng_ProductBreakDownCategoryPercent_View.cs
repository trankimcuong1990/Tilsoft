//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductBreakDownMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductBreakDownMng_ProductBreakDownCategoryPercent_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductBreakDownMng_ProductBreakDownCategoryPercent_View()
        {
            this.ProductBreakDownMng_ProductBreakDownCategoryImage_View = new HashSet<ProductBreakDownMng_ProductBreakDownCategoryImage_View>();
            this.ProductBreakDownMng_ProductBreakDownCategoryType_View = new HashSet<ProductBreakDownMng_ProductBreakDownCategoryType_View>();
        }
    
        public int ProductBreakDownCategoryID { get; set; }
        public string ProductBreakDownCategoryNM { get; set; }
        public Nullable<int> ProductBreakDownID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Unit { get; set; }
        public string Remark { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<int> ProductBreakDownCalculationTypeID { get; set; }
        public string ProductBreakDownCalculationTypeNM { get; set; }
        public long PrimaryID { get; set; }
        public Nullable<int> OptionTotalID { get; set; }
        public Nullable<int> OptionToGetPriceID { get; set; }
        public Nullable<decimal> QuantityPercent { get; set; }
        public string OptionToGetPriceNM { get; set; }
    
        public virtual ProductBreakDownMng_ProductBreakDown_View ProductBreakDownMng_ProductBreakDown_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBreakDownMng_ProductBreakDownCategoryImage_View> ProductBreakDownMng_ProductBreakDownCategoryImage_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBreakDownMng_ProductBreakDownCategoryType_View> ProductBreakDownMng_ProductBreakDownCategoryType_View { get; set; }
    }
}
