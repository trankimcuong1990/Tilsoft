//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.LedgerAccountMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class LedgerAccount
    {
        public int LedgerAccountID { get; set; }
        public string AccountNo { get; set; }
        public string VietnameseNM { get; set; }
        public string EnglishNM { get; set; }
        public Nullable<int> ParentID { get; set; }
        public Nullable<decimal> OpeningDebit { get; set; }
        public Nullable<decimal> OpeningCredit { get; set; }
        public Nullable<decimal> ClosingDebit { get; set; }
        public Nullable<decimal> ClosingCredit { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
    }
}
