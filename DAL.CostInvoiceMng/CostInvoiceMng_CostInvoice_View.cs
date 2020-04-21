//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.CostInvoiceMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class CostInvoiceMng_CostInvoice_View
    {
        public CostInvoiceMng_CostInvoice_View()
        {
            this.CostInvoiceMng_CostInvoiceDetail_View = new HashSet<CostInvoiceMng_CostInvoiceDetail_View>();
        }
    
        public int CostInvoiceID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> BookingID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceRefNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string Currency { get; set; }
        public string Condition { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> PaymentTermID { get; set; }
        public Nullable<int> DeliveryTermID { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public string EurofarInvoiceNo { get; set; }
        public string BLNo { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string DeliveryTypeNM { get; set; }
        public string PaymentTypeNM { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string CostType { get; set; }
    
        public virtual ICollection<CostInvoiceMng_CostInvoiceDetail_View> CostInvoiceMng_CostInvoiceDetail_View { get; set; }
    }
}
