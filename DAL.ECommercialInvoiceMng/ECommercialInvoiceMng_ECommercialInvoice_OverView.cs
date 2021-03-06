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
    
    public partial class ECommercialInvoiceMng_ECommercialInvoice_OverView
    {
        public ECommercialInvoiceMng_ECommercialInvoice_OverView()
        {
            this.ECommercialInvoiceMng_ECommercialInvoiceDescription_OverView = new HashSet<ECommercialInvoiceMng_ECommercialInvoiceDescription_OverView>();
            this.ECommercialInvoiceMng_ECommercialInvoiceDetail_OverView = new HashSet<ECommercialInvoiceMng_ECommercialInvoiceDetail_OverView>();
            this.ECommercialInvoiceMng_ECommercialInvoiceExtDetail_OverView = new HashSet<ECommercialInvoiceMng_ECommercialInvoiceExtDetail_OverView>();
            this.ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_OverView = new HashSet<ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_OverView>();
            this.ECommercialInvoiceMng_WarehouseImport_OverView = new HashSet<ECommercialInvoiceMng_WarehouseImport_OverView>();
            this.ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_OverView = new HashSet<ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_OverView>();
            this.ECommercialInvoiceMng_Booking_OverView = new HashSet<ECommercialInvoiceMng_Booking_OverView>();
            this.ECommercialInvoiceMng_CreditNote_OverView = new HashSet<ECommercialInvoiceMng_CreditNote_OverView>();
            this.ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_OverView = new HashSet<ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_OverView>();
            this.ECommercialInvoiceMng_WarehouseInvoiceProductDetail_OverView = new HashSet<ECommercialInvoiceMng_WarehouseInvoiceProductDetail_OverView>();
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
        public string ClientInvoiceNo { get; set; }
        public string Season { get; set; }
        public Nullable<int> ParentID { get; set; }
        public Nullable<bool> IsDonePayment { get; set; }
        public Nullable<int> BookingID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string DefaultCiReport { get; set; }
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
        public Nullable<int> ParentTypeOfInvoice { get; set; }
        public string IsConfirmedText { get; set; }
        public string TypeOfInvoiceText { get; set; }
        public string BLNo { get; set; }
        public string PoLName { get; set; }
        public string StatusPayment { get; set; }
    
        public virtual ICollection<ECommercialInvoiceMng_ECommercialInvoiceDescription_OverView> ECommercialInvoiceMng_ECommercialInvoiceDescription_OverView { get; set; }
        public virtual ICollection<ECommercialInvoiceMng_ECommercialInvoiceDetail_OverView> ECommercialInvoiceMng_ECommercialInvoiceDetail_OverView { get; set; }
        public virtual ICollection<ECommercialInvoiceMng_ECommercialInvoiceExtDetail_OverView> ECommercialInvoiceMng_ECommercialInvoiceExtDetail_OverView { get; set; }
        public virtual ICollection<ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_OverView> ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_OverView { get; set; }
        public virtual ICollection<ECommercialInvoiceMng_WarehouseImport_OverView> ECommercialInvoiceMng_WarehouseImport_OverView { get; set; }
        public virtual ICollection<ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_OverView> ECommercialInvoiceMng_WarehouseInvoiceSparepartDetail_OverView { get; set; }
        public virtual ICollection<ECommercialInvoiceMng_Booking_OverView> ECommercialInvoiceMng_Booking_OverView { get; set; }
        public virtual ICollection<ECommercialInvoiceMng_CreditNote_OverView> ECommercialInvoiceMng_CreditNote_OverView { get; set; }
        public virtual ICollection<ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_OverView> ECommercialInvoiceMng_ECommercialInvoiceContainerTransport_OverView { get; set; }
        public virtual ICollection<ECommercialInvoiceMng_WarehouseInvoiceProductDetail_OverView> ECommercialInvoiceMng_WarehouseInvoiceProductDetail_OverView { get; set; }
    }
}
