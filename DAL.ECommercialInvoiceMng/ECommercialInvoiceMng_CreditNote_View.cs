//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ECommercialInvoiceMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ECommercialInvoiceMng_CreditNote_View
    {
        public int CreditNoteID { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public string CreditNoteNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string RefNo { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
    
        public virtual ECommercialInvoiceMng_ECommercialInvoice_View ECommercialInvoiceMng_ECommercialInvoice_View { get; set; }
    }
}
