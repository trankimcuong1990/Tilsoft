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
    
    public partial class SampleProgressImage
    {
        public int SampleProgressImageID { get; set; }
        public Nullable<int> SampleProgressID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
    
        public virtual SampleProgress SampleProgress { get; set; }
    }
}
