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
    
    public partial class SampleProductPackagingMaterial
    {
        public int SampleProductPackagingMaterialID { get; set; }
        public Nullable<int> SampleProductPackagingMaterialTypeID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string Remark { get; set; }
        public Nullable<int> SampleProductID { get; set; }
    
        public virtual SampleProduct SampleProduct { get; set; }
    }
}
