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
    
    public partial class FactoryPayment2BalanceMng_Detail_View
    {
        public long PrimaryID { get; set; }
        public string Season { get; set; }
        public string ReceiptNo { get; set; }
        public string CreditNoteNo { get; set; }
        public Nullable<System.DateTime> IssuedDate { get; set; }
        public Nullable<decimal> IncreasingMutation { get; set; }
        public Nullable<decimal> DecreasingMutation { get; set; }
        public Nullable<int> SupplierID { get; set; }
    }
}
