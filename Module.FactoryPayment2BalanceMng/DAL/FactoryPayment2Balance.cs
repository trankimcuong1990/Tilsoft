//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryPayment2BalanceMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryPayment2Balance
    {
        public int FactoryPayment2BalanceID { get; set; }
        public string Season { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<decimal> BeginBalance { get; set; }
        public Nullable<decimal> EndBalance { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> isClosed { get; set; }
    }
}
