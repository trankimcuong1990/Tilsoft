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
    
    public partial class ECommercialInvoice
    {
        public ECommercialInvoice()
        {
            this.ECommercialInvoiceExtDetail = new HashSet<ECommercialInvoiceExtDetail>();
            this.ECommercialInvoiceDescription = new HashSet<ECommercialInvoiceDescription>();
            this.ECommercialInvoiceDetail = new HashSet<ECommercialInvoiceDetail>();
            this.ECommercialInvoiceSparepartDetail = new HashSet<ECommercialInvoiceSparepartDetail>();
            this.WarehouseInvoiceProductDetail = new HashSet<WarehouseInvoiceProductDetail>();
            this.WarehouseInvoiceSparepartDetail = new HashSet<WarehouseInvoiceSparepartDetail>();
            this.ECommercialInvoiceContainerTransport = new HashSet<ECommercialInvoiceContainerTransport>();
            this.ECommercialInvoiceSampleDetail = new HashSet<ECommercialInvoiceSampleDetail>();
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
        public string BizBloqOrderNo { get; set; }
    
        public virtual ICollection<ECommercialInvoiceExtDetail> ECommercialInvoiceExtDetail { get; set; }
        public virtual ICollection<ECommercialInvoiceDescription> ECommercialInvoiceDescription { get; set; }
        public virtual ICollection<ECommercialInvoiceDetail> ECommercialInvoiceDetail { get; set; }
        public virtual ICollection<ECommercialInvoiceSparepartDetail> ECommercialInvoiceSparepartDetail { get; set; }
        public virtual ICollection<WarehouseInvoiceProductDetail> WarehouseInvoiceProductDetail { get; set; }
        public virtual ICollection<WarehouseInvoiceSparepartDetail> WarehouseInvoiceSparepartDetail { get; set; }
        public virtual ICollection<ECommercialInvoiceContainerTransport> ECommercialInvoiceContainerTransport { get; set; }
        public virtual ICollection<ECommercialInvoiceSampleDetail> ECommercialInvoiceSampleDetail { get; set; }
        public virtual Client Client { get; set; }
    }
}
