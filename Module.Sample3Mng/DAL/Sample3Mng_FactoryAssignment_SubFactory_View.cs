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
    
    public partial class Sample3Mng_FactoryAssignment_SubFactory_View
    {
        public int SampleProductSubFactoryID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<int> SampleProductID { get; set; }
    
        public virtual Sample3Mng_FactoryAssignment_Product_View Sample3Mng_FactoryAssignment_Product_View { get; set; }
    }
}
