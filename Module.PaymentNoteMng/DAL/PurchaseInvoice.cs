//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PaymentNoteMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseInvoice
    {
        public int PurchaseInvoiceID { get; set; }
        public string PurchaseInvoiceUD { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<int> PurchaseInvoiceTypeID { get; set; }
        public Nullable<System.DateTime> PurchaseInvoiceDate { get; set; }
        public Nullable<System.DateTime> PostingDate { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public Nullable<int> PurchaseInvoiceStatusID { get; set; }
        public string AttachedFile { get; set; }
        public string Currency { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Remark { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public Nullable<int> SetStatusBy { get; set; }
        public Nullable<System.DateTime> SetStatusDate { get; set; }
        public Nullable<System.DateTime> FinishDate { get; set; }
        public Nullable<decimal> VATAmount { get; set; }
        public Nullable<int> SupplierPaymentTerm { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
    }
}
