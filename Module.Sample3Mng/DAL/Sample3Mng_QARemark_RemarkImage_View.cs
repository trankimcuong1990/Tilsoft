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
    
    public partial class Sample3Mng_QARemark_RemarkImage_View
    {
        public int SampleQARemarkImageID { get; set; }
        public Nullable<int> SampleQARemarkID { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
    
        public virtual Sample3Mng_QARemark_Remark_View Sample3Mng_QARemark_Remark_View { get; set; }
    }
}