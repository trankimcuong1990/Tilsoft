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
    
    public partial class SamplePackaging
    {
        public int SamplePackagingID { get; set; }
        public Nullable<int> SampleProductID { get; set; }
        public string CartonBoxDimL { get; set; }
        public string CartonBoxDimW { get; set; }
        public string CartonBoxDimH { get; set; }
        public string Remark { get; set; }
    
        public virtual SampleProduct SampleProduct { get; set; }
    }
}