//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryPayment2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryPayment2Detail
    {
        public int FactoryPayment2DetailID { get; set; }
        public Nullable<int> FactoryPayment2ID { get; set; }
        public Nullable<int> FactoryInvoiceID { get; set; }
        public Nullable<decimal> DPDeductedAmount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public string Remark { get; set; }
    
        public virtual FactoryPayment2 FactoryPayment2 { get; set; }
    }
}