//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.AVFOutwardInvoiceMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class AVFOutwardInvoiceMng_AVFOutwardInvoice_View
    {
        public AVFOutwardInvoiceMng_AVFOutwardInvoice_View()
        {
            this.AVFOutwardInvoiceMng_AVFOutwardInvoiceDetail_View = new HashSet<AVFOutwardInvoiceMng_AVFOutwardInvoiceDetail_View>();
        }
    
        public int AVFOutwardInvoiceID { get; set; }
        public string SerialNo { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string Season { get; set; }
        public string CreditorNM { get; set; }
        public string CreditorTaxCode { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> VATAmount { get; set; }
        public int TaxRate { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string CustomsAccount { get; set; }
        public string POD { get; set; }
        public string POA { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatorName2 { get; set; }
    
        public virtual ICollection<AVFOutwardInvoiceMng_AVFOutwardInvoiceDetail_View> AVFOutwardInvoiceMng_AVFOutwardInvoiceDetail_View { get; set; }
    }
}
