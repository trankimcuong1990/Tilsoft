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
    
    public partial class ClientPaymentMng_ClientPaymentDeduction_View
    {
        public int ClientPaymentDeductionID { get; set; }
        public Nullable<int> ClientPaymentDetailID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<decimal> ToBePaidAmount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public decimal DeductedAmount { get; set; }
    
        public virtual ClientPaymentMng_ClientPaymentDetail_View ClientPaymentMng_ClientPaymentDetail_View { get; set; }
    }
}