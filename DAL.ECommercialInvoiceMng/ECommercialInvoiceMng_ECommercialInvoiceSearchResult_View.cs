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
    
    public partial class ECommercialInvoiceMng_ECommercialInvoiceSearchResult_View
    {
        public int ECommercialInvoiceID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string InvoiceNo { get; set; }
        public string RefNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<int> AccountNo { get; set; }
        public string MultiversNo { get; set; }
        public string MVBookingNo { get; set; }
        public string Conditions { get; set; }
        public Nullable<bool> IsLC { get; set; }
        public string LCRefNo { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> VATRate { get; set; }
        public Nullable<decimal> NetAmount { get; set; }
        public Nullable<decimal> VATAmount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string ClientAddress { get; set; }
        public string DeliveryTerm { get; set; }
        public string PaymentTerm { get; set; }
        public string ClientInvoiceNo { get; set; }
        public Nullable<int> TypeOfInvoice { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string IsConfirmedText { get; set; }
        public string TypeOfInvoiceText { get; set; }
        public string CreatorName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string PrinterName { get; set; }
        public Nullable<System.DateTime> PrintedDate { get; set; }
        public Nullable<bool> IsPrinted { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public string ConfirmerName { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public string Remark { get; set; }
        public string Season { get; set; }
        public string FactoryInvoiceNo { get; set; }
        public string BLNo { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string PurchasingInvoiceNo { get; set; }
        public Nullable<decimal> NetAmountEUR { get; set; }
        public Nullable<decimal> VATAmountEUR { get; set; }
        public Nullable<decimal> TotalAmountEUR { get; set; }
        public Nullable<decimal> ExRate { get; set; }
    }
}
