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
    
    public partial class FactoryRawMaterialMng_FactoryRawMaterialQualityPerson_View
    {
        public int FactoryRawMaterialQualityPersonID { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string EmployeeNM { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual FactoryRawMaterialMng_FactoryRawMaterial_View FactoryRawMaterialMng_FactoryRawMaterial_View { get; set; }
        public virtual FactoryRawMaterialMng_FactoryRawMaterialSearch_View FactoryRawMaterialMng_FactoryRawMaterialSearch_View { get; set; }
    }
}
