//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.SampleItemMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryBreakdown
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FactoryBreakdown()
        {
            this.FactoryBreakdownDetail = new HashSet<FactoryBreakdownDetail>();
        }
    
        public int FactoryBreakdownID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public string PackingDimensionL { get; set; }
        public string PackingDimensionW { get; set; }
        public string PackingDimensionH { get; set; }
        public string CushionDimensionL { get; set; }
        public string CushionDimensionW { get; set; }
        public string CushionDimensionH { get; set; }
        public string Remark { get; set; }
        public Nullable<decimal> IndicatedPrice { get; set; }
        public string Season { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public Nullable<int> MaterialTypeID { get; set; }
        public Nullable<int> MaterialColorID { get; set; }
        public Nullable<int> FrameMaterialID { get; set; }
        public Nullable<int> FrameMaterialColorID { get; set; }
        public Nullable<int> SubMaterialID { get; set; }
        public Nullable<int> SubMaterialColorID { get; set; }
        public Nullable<int> BackCushionID { get; set; }
        public Nullable<int> SeatCushionID { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public Nullable<int> FSCTypeID { get; set; }
        public Nullable<int> FSCPercentID { get; set; }
    
        public virtual SampleProduct SampleProduct { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FactoryBreakdownDetail> FactoryBreakdownDetail { get; set; }
    }
}
