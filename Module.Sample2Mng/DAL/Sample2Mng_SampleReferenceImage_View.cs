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
    
    public partial class Sample2Mng_SampleReferenceImage_View
    {
        public int SampleReferenceImageID { get; set; }
        public Nullable<int> SampleProductID { get; set; }
        public string FileUD { get; set; }
        public string BringInBy { get; set; }
        public Nullable<System.DateTime> BringInDate { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string UpdatorName { get; set; }
    
        public virtual Sample2Mng_SampleProduct_View Sample2Mng_SampleProduct_View { get; set; }
    }
}