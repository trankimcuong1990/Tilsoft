//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PaymentNoteMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupplierDeposit
    {
        public int SupplierDepositID { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public string Year { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Currency { get; set; }
    }
}