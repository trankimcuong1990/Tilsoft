//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchaseInvoiceMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseInvoiceMng_PurchaseInvoice_SearchView
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
        public Nullable<decimal> VAT { get; set; }
        public string Remark { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> SetStatusBy { get; set; }
        public Nullable<System.DateTime> SetStatusDate { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string StatusName { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string PurchaseInvoiceStatusNM { get; set; }
        public string PurchaseInvoiceTypeNM { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> VATAmount { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
    }
}
