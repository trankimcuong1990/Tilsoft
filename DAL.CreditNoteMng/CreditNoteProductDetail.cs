//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.CreditNoteMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class CreditNoteProductDetail
    {
        public int CreditNoteProductDetailID { get; set; }
        public Nullable<int> ECommercialInvoiceDetailID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string Remark { get; set; }
    
        public virtual CreditNote CreditNote { get; set; }
    }
}
