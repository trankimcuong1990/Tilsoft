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
    
    public partial class Sample3Mng_SampleProductOverview_QARemark_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sample3Mng_SampleProductOverview_QARemark_View()
        {
            this.Sample3Mng_SampleProductOverview_QARemarkImage_View = new HashSet<Sample3Mng_SampleProductOverview_QARemarkImage_View>();
        }
    
        public int SampleQARemarkID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> SampleProductID { get; set; }
        public string ThumbnailLocation { get; set; }
    
        public virtual Sample3Mng_SampleProductOverview_Product_View Sample3Mng_SampleProductOverview_Product_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sample3Mng_SampleProductOverview_QARemarkImage_View> Sample3Mng_SampleProductOverview_QARemarkImage_View { get; set; }
    }
}
