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
    
    public partial class SampleProductMinuteImage
    {
        public int SampleProductMinuteImageID { get; set; }
        public Nullable<int> SampleProductID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public Nullable<int> SampleProductMinuteID { get; set; }
    
        public virtual SampleProductMinute SampleProductMinute { get; set; }
    }
}
