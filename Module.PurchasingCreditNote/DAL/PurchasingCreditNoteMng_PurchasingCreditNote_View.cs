//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchasingCreditNote.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchasingCreditNoteMng_PurchasingCreditNote_View
    {
        public PurchasingCreditNoteMng_PurchasingCreditNote_View()
        {
            this.PurchasingCreditNoteMng_PurchasingCreditNoteDetail_View = new HashSet<PurchasingCreditNoteMng_PurchasingCreditNoteDetail_View>();
            this.PurchasingCreditNoteMng_PurchasingCreditNoteExtendDetail_View = new HashSet<PurchasingCreditNoteMng_PurchasingCreditNoteExtendDetail_View>();
            this.PurchasingCreditNoteMng_PurchasingCreditNoteSparepartDetail_View = new HashSet<PurchasingCreditNoteMng_PurchasingCreditNoteSparepartDetail_View>();
        }
    
        public int PurchasingCreditNoteID { get; set; }
        public Nullable<int> PurchasingInvoiceID { get; set; }
        public string CreditNoteNo { get; set; }
        public Nullable<System.DateTime> CreditNoteDate { get; set; }
        public string Remark { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string BLNo { get; set; }
        public Nullable<System.DateTime> ShipedDate { get; set; }
        public string SupplierNM { get; set; }
        public string ForwarderNM { get; set; }
        public string Feeder { get; set; }
        public string POLName { get; set; }
        public string PODName { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public string Season { get; set; }
        public Nullable<int> CreditNoteType { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public string InvoiceStatusText { get; set; }
        public string ConfirmerName { get; set; }
        public string CreatorName2 { get; set; }
        public string UpdatorName2 { get; set; }
    
        public virtual ICollection<PurchasingCreditNoteMng_PurchasingCreditNoteDetail_View> PurchasingCreditNoteMng_PurchasingCreditNoteDetail_View { get; set; }
        public virtual ICollection<PurchasingCreditNoteMng_PurchasingCreditNoteExtendDetail_View> PurchasingCreditNoteMng_PurchasingCreditNoteExtendDetail_View { get; set; }
        public virtual ICollection<PurchasingCreditNoteMng_PurchasingCreditNoteSparepartDetail_View> PurchasingCreditNoteMng_PurchasingCreditNoteSparepartDetail_View { get; set; }
    }
}
