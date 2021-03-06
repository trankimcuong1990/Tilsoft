//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Sample3Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sample3Mng_SampleProductOverview_Product_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sample3Mng_SampleProductOverview_Product_View()
        {
            this.Sample3Mng_SampleProductOverview_InternalRemark_View = new HashSet<Sample3Mng_SampleProductOverview_InternalRemark_View>();
            this.Sample3Mng_SampleProductOverview_ItemLocation_View = new HashSet<Sample3Mng_SampleProductOverview_ItemLocation_View>();
            this.Sample3Mng_SampleProductOverview_Progress_View = new HashSet<Sample3Mng_SampleProductOverview_Progress_View>();
            this.Sample3Mng_SampleProductOverview_QARemark_View = new HashSet<Sample3Mng_SampleProductOverview_QARemark_View>();
            this.Sample3Mng_SampleProductOverview_ReferenceImage_View = new HashSet<Sample3Mng_SampleProductOverview_ReferenceImage_View>();
            this.Sample3Mng_SampleProductOverview_SubFactory_View = new HashSet<Sample3Mng_SampleProductOverview_SubFactory_View>();
            this.Sample3Mng_SampleProductOverview_TechnicalDrawing_View = new HashSet<Sample3Mng_SampleProductOverview_TechnicalDrawing_View>();
        }
    
        public int SampleProductID { get; set; }
        public string SampleProductUD { get; set; }
        public string SampleProductStatusNM { get; set; }
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
        public string BackCushionThickness { get; set; }
        public string BackCushionSpecification { get; set; }
        public string SeatCushionDescription { get; set; }
        public string SeatCushionThickness { get; set; }
        public string SeatCushionSpecification { get; set; }
        public string CushionColorDescription { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string ItemPerBox { get; set; }
        public string BoxPerSet { get; set; }
        public string FinishedImageThumbnailLocation { get; set; }
        public string FinishedImageFileLocation { get; set; }
        public string VNSuggestedFactoryUD { get; set; }
        public Nullable<System.DateTime> FactoryDeadline { get; set; }
        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
        public Nullable<int> LoadAbility20DC { get; set; }
        public Nullable<int> LoadAbility40DC { get; set; }
        public Nullable<int> LoadAbility40HC { get; set; }
        public string CartonBoxDimL { get; set; }
        public string CartonBoxDimW { get; set; }
        public string CartonBoxDimH { get; set; }
        public string NetWeight { get; set; }
        public string GrossWeight { get; set; }
        public string QntInBox { get; set; }
        public Nullable<decimal> IndicatedPrice { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string Updator { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> SampleOrderID { get; set; }
        public Nullable<bool> IsTDNeeded { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sample3Mng_SampleProductOverview_InternalRemark_View> Sample3Mng_SampleProductOverview_InternalRemark_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sample3Mng_SampleProductOverview_ItemLocation_View> Sample3Mng_SampleProductOverview_ItemLocation_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sample3Mng_SampleProductOverview_Progress_View> Sample3Mng_SampleProductOverview_Progress_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sample3Mng_SampleProductOverview_QARemark_View> Sample3Mng_SampleProductOverview_QARemark_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sample3Mng_SampleProductOverview_ReferenceImage_View> Sample3Mng_SampleProductOverview_ReferenceImage_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sample3Mng_SampleProductOverview_SubFactory_View> Sample3Mng_SampleProductOverview_SubFactory_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sample3Mng_SampleProductOverview_TechnicalDrawing_View> Sample3Mng_SampleProductOverview_TechnicalDrawing_View { get; set; }
    }
}
