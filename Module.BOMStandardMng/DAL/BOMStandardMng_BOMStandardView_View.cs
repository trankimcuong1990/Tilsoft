//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.BOMStandardMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class BOMStandardMng_BOMStandardView_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOMStandardMng_BOMStandardView_View()
        {
            this.BOMStandardMng_BOMStandardView_View1 = new HashSet<BOMStandardMng_BOMStandardView_View>();
        }
    
        public int BOMStandardID { get; set; }
        public Nullable<int> ParentBOMStandardID { get; set; }
        public string ProductArticleCode { get; set; }
        public string ProductDescription { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public Nullable<decimal> QtyByUnit { get; set; }
        public string UnitNM { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public string WorkCenterNM { get; set; }
        public string OPSequenceNM { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> ProductionItemTypeID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOMStandardMng_BOMStandardView_View> BOMStandardMng_BOMStandardView_View1 { get; set; }
        public virtual BOMStandardMng_BOMStandardView_View BOMStandardMng_BOMStandardView_View2 { get; set; }
    }
}
