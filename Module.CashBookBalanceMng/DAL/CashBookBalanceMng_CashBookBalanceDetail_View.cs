//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.CashBookBalanceMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CashBookBalanceMng_CashBookBalanceDetail_View
    {
        public int CashBookBalanceDetailID { get; set; }
        public Nullable<int> CashBookBalanceID { get; set; }
        public Nullable<int> CashBookPaidByID { get; set; }
        public Nullable<decimal> VNDAmount { get; set; }
        public string PaidByNM { get; set; }
    
        public virtual CashBookBalanceMng_CashBookBalance_View CashBookBalanceMng_CashBookBalance_View { get; set; }
    }
}
