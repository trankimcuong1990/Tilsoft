//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Sample2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sample2Mng_SampleProductMinute_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sample2Mng_SampleProductMinute_View()
        {
            this.Sample2Mng_SampleProductMinuteImage_View = new HashSet<Sample2Mng_SampleProductMinuteImage_View>();
        }
    
        public int SampleProductMinuteID { get; set; }
        public Nullable<int> SampleProductID { get; set; }
        public string MeetingMinute { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
    
        public virtual Sample2Mng_SampleProduct_View Sample2Mng_SampleProduct_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sample2Mng_SampleProductMinuteImage_View> Sample2Mng_SampleProductMinuteImage_View { get; set; }
    }
}
