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
    
    public partial class SampleReferenceImage
    {
        public int SampleReferenceImageID { get; set; }
        public Nullable<int> SampleProductID { get; set; }
        public string FileUD { get; set; }
        public string BringInBy { get; set; }
        public Nullable<System.DateTime> BringInDate { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
    
        public virtual SampleProduct SampleProduct { get; set; }
    }
}
