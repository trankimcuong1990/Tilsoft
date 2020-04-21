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
    
    public partial class ECommercialInvoiceMng_ECommercialInvoice_View
    {
        public ECommercialInvoiceMng_ECommercialInvoice_View()
        {
            this.ECommercialInvoiceMng_ECommercialInvoiceDetail_View = new HashSet<ECommercialInvoiceMng_ECommercialInvoiceDetail_View>();
            this.ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_View = new HashSet<ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_View>();
        }
    
        public int ECommercialInvoiceID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string InvoiceNo { get; set; }
        public string RefNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<int> AccountNo { get; set; }
        public string MultiversNo { get; set; }
        public string MVBookingNo { get; set; }
        public string Conditions { get; set; }
        public Nullable<bool> IsLC { get; set; }
        public string LCRefNo { get; set; }
        public Nullable<int> DeliveryTermID { get; set; }
        public Nullable<int> PaymentTermID { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> VATRate { get; set; }
        public Nullable<decimal> NetAmount { get; set; }
        public Nullable<decimal> VATAmount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public string DiscountDescription { get; set; }
        public Nullable<decimal> SeaFreightAmount { get; set; }
        public string SeaFreightDescription { get; set; }
        public Nullable<decimal> OtherAmount { get; set; }
        public string OtherDescription { get; set; }
        public string ClientAddress { get; set; }
        public string DeliveryTerm { get; set; }
        public string PaymentTerm { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> PrintedBy { get; set; }
        public Nullable<System.DateTime> PrintedDate { get; set; }
        public Nullable<bool> IsPrinted { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public string Remark { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> TypeOfInvoice { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string VATNo { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string CreditNoteNo { get; set; }
        public string DeliveryTypeNM { get; set; }
        public string PaymentTypeNM { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string PrinterName { get; set; }
        public string ConfirmerName { get; set; }
        public string ClientInvoiceNo { get; set; }
        public string Season { get; set; }
    
        public virtual ICollection<ECommercialInvoiceMng_ECommercialInvoiceDetail_View> ECommercialInvoiceMng_ECommercialInvoiceDetail_View { get; set; }
        public virtual ICollection<ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_View> ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_View { get; set; }
    }
}