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
    
    public partial class SubSupplierContract
    {
        public int SubSupplierContractID { get; set; }
        public Nullable<int> SubSupplierID { get; set; }
        public string ContractNo { get; set; }
        public string ContractFile { get; set; }
        public string Remark { get; set; }
        public Nullable<int> ContractTypeID { get; set; }
        public Nullable<bool> IsSigned { get; set; }
    
        public virtual FactoryRawMaterial FactoryRawMaterial { get; set; }
    }
}
