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
    
    public partial class FactoryRawMaterialMng_SupplierContactQuickInfo_View
    {
        public int SupplierContactQuickInfoId { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
    
        public virtual FactoryRawMaterialMng_FactoryRawMaterial_View FactoryRawMaterialMng_FactoryRawMaterial_View { get; set; }
    }
}
