//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ClientPaymentMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientPaymentMng_SaleOrderForDeductionSearchResult_View
    {
        public long PrimaryID { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DeductedAmount { get; set; }
        public Nullable<decimal> ToBePaid { get; set; }
    }
}
