//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductionItemGroup.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductionItemMng_ProductionItemGroup_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductionItemMng_ProductionItemGroup_View()
        {
            this.ProductionItemGroupNotification_View = new HashSet<ProductionItemGroupNotification_View>();
            this.ProductionItemGroupStockNotification_View = new HashSet<ProductionItemGroupStockNotification_View>();
        }
    
        public int ProductionItemGroupID { get; set; }
        public string ProductionItemGroupUD { get; set; }
        public string ProductionItemGroupNM { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatorNM { get; set; }
        public Nullable<decimal> WastagePercent { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionItemGroupNotification_View> ProductionItemGroupNotification_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionItemGroupStockNotification_View> ProductionItemGroupStockNotification_View { get; set; }
    }
}
