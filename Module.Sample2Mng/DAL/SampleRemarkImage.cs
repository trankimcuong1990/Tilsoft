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
    
    public partial class SampleRemarkImage
    {
        public int SampleRemarkImageID { get; set; }
        public Nullable<int> SampleRemarkID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
    
        public virtual SampleRemark SampleRemark { get; set; }
    }
}
