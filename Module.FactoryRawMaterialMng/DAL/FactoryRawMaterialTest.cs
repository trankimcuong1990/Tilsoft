//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryRawMaterialMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryRawMaterialTest
    {
        public int FactoryRawMaterialTestID { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public string Description { get; set; }
    
        public virtual FactoryRawMaterial FactoryRawMaterial { get; set; }
    }
}