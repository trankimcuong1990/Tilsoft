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
    
    public partial class Sample2Mng_SampleProgressImage_View
    {
        public int SampleProgressImageID { get; set; }
        public Nullable<int> SampleProgressID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
    
        public virtual Sample2Mng_SampleProgress_View Sample2Mng_SampleProgress_View { get; set; }
    }
}