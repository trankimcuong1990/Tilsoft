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
    
    public partial class CreditNoteMng_CreditNote_View
    {
        public CreditNoteMng_CreditNote_View()
        {
            this.CreditNoteMng_CreditNoteDetail_View = new HashSet<CreditNoteMng_CreditNoteDetail_View>();
            this.CreditNoteMng_CreditNoteProductDetail_View = new HashSet<CreditNoteMng_CreditNoteProductDetail_View>();
            this.CreditNoteMng_CreditNoteSparepartDetail_View = new HashSet<CreditNoteMng_CreditNoteSparepartDetail_View>();
        }
    
        public int CreditNoteID { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public string CreditNoteNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string RefNo { get; set; }
        public string Remark { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string VATNo { get; set; }
        public string TelePhone { get; set; }
        public string Fax { get; set; }
        public string ClientAddress { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string AccountNo { get; set; }
        public string Conditions { get; set; }
        public string LCRefNo { get; set; }
        public string Currency { get; set; }
        public string DeliveryTerm { get; set; }
        public string PaymentTerm { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public string ConfirmerName { get; set; }
    
        public virtual ICollection<CreditNoteMng_CreditNoteDetail_View> CreditNoteMng_CreditNoteDetail_View { get; set; }
        public virtual ICollection<CreditNoteMng_CreditNoteProductDetail_View> CreditNoteMng_CreditNoteProductDetail_View { get; set; }
        public virtual ICollection<CreditNoteMng_CreditNoteSparepartDetail_View> CreditNoteMng_CreditNoteSparepartDetail_View { get; set; }
    }
}