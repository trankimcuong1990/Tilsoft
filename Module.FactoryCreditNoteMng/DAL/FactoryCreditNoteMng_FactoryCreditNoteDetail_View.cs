//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryCreditNoteMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryCreditNoteMng_FactoryCreditNoteDetail_View
    {
        public int FactoryCreditNoteDetailID { get; set; }
        public Nullable<int> FactoryCreditNoteID { get; set; }
        public Nullable<int> FactoryInvoiceID { get; set; }
        public Nullable<decimal> CreditAmount { get; set; }
        public string Remark { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
    
        public virtual FactoryCreditNoteMng_FactoryCreditNote_View FactoryCreditNoteMng_FactoryCreditNote_View { get; set; }
    }
}
