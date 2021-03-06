//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.CushionColorMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CushionColorMng_CushionColor_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CushionColorMng_CushionColor_View()
        {
            this.CushionColorMng_CushionColorProductGroup_View = new HashSet<CushionColorMng_CushionColorProductGroup_View>();
            this.CushionColorMng_CushionColorTestReport_View = new HashSet<CushionColorMng_CushionColorTestReport_View>();
            this.CushionColorMng_CushionTesting_View = new HashSet<CushionColorMng_CushionTesting_View>();
        }
    
        public int CushionColorID { get; set; }
        public string CushionColorUD { get; set; }
        public string CushionColorNM { get; set; }
        public string Season { get; set; }
        public Nullable<bool> IsStandard { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public Nullable<int> CushionTypeID { get; set; }
        public Nullable<int> DisplayIndex { get; set; }
        public string ImageFile { get; set; }
        public string TestReportFile1 { get; set; }
        public string TestReportFile2 { get; set; }
        public string TestReportFile3 { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string FriendlyName1 { get; set; }
        public string FileLocation1 { get; set; }
        public string FriendlyName2 { get; set; }
        public string FileLocation2 { get; set; }
        public string FriendlyName3 { get; set; }
        public string FileLocation3 { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string UpdatorName { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName2 { get; set; }
        public string CreatorName2 { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string ProductGroupNM { get; set; }
        public string ImageFile_FullSizeUrl { get; set; }
        public string CushionTypeNM { get; set; }
        public Nullable<int> Total { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CushionColorMng_CushionColorProductGroup_View> CushionColorMng_CushionColorProductGroup_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CushionColorMng_CushionColorTestReport_View> CushionColorMng_CushionColorTestReport_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CushionColorMng_CushionTesting_View> CushionColorMng_CushionTesting_View { get; set; }
    }
}
