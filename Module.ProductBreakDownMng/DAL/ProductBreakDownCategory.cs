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
    
    public partial class ProductBreakDownCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductBreakDownCategory()
        {
            this.FactoryProductBreakDownCategory = new HashSet<FactoryProductBreakDownCategory>();
            this.ProductBreakDownCategoryImage = new HashSet<ProductBreakDownCategoryImage>();
            this.ProductBreakDownCategoryType = new HashSet<ProductBreakDownCategoryType>();
        }
    
        public int ProductBreakDownCategoryID { get; set; }
        public string ProductBreakDownCategoryNM { get; set; }
        public Nullable<int> ProductBreakDownID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Unit { get; set; }
        public Nullable<int> ProductBreakDownCalculationTypeID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string ImageFile { get; set; }
        public Nullable<int> OptionTotalID { get; set; }
        public Nullable<int> OptionToGetPriceID { get; set; }
        public Nullable<decimal> QuantityPercent { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FactoryProductBreakDownCategory> FactoryProductBreakDownCategory { get; set; }
        public virtual ProductBreakDown ProductBreakDown { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBreakDownCategoryImage> ProductBreakDownCategoryImage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBreakDownCategoryType> ProductBreakDownCategoryType { get; set; }
    }
}
