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
    
    public partial class SampleItemMng_FactoryBreakdown_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SampleItemMng_FactoryBreakdown_View()
        {
            this.SampleItemMng_FactoryBreakdownDetail_View = new HashSet<SampleItemMng_FactoryBreakdownDetail_View>();
        }
    
        public int FactoryBreakdownID { get; set; }
        public Nullable<int> SampleProductID { get; set; }
        public Nullable<int> ClientID { get; set; }
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
        public string SampleProductUD { get; set; }
        public string ArticleDescription { get; set; }
        public string MaterialDescription { get; set; }
        public string MaterialTypeDescription { get; set; }
        public string MaterialColorDescription { get; set; }
        public string Material2Description { get; set; }
        public string Material2TypeDescription { get; set; }
        public string Material2ColorDescription { get; set; }
        public string Material3Description { get; set; }
        public string Material3TypeDescription { get; set; }
        public string Material3ColorDescription { get; set; }
        public string BackCushionDescription { get; set; }
        public string SeatCushionDescription { get; set; }
        public string CushionColorDescription { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string MaterialNM { get; set; }
        public string MaterialTypeNM { get; set; }
        public string MaterialColorNM { get; set; }
        public string FrameMaterialNM { get; set; }
        public string FrameMaterialColorNM { get; set; }
        public string SubMaterialNM { get; set; }
        public string SubMaterialColorNM { get; set; }
        public string BackCushionNM { get; set; }
        public string SeatCushionNM { get; set; }
        public string CushionColorNM { get; set; }
        public string FSCTypeNM { get; set; }
        public string FSCPercentNM { get; set; }
        public string ClientUD { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string SampleOrderUD { get; set; }
    
        public virtual SampleItemMng_SampleProduct_View SampleItemMng_SampleProduct_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SampleItemMng_FactoryBreakdownDetail_View> SampleItemMng_FactoryBreakdownDetail_View { get; set; }
    }
}