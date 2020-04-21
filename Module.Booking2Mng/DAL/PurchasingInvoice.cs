//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Booking2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchasingInvoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchasingInvoice()
        {
            this.PurchasingInvoiceDetail = new HashSet<PurchasingInvoiceDetail>();
            this.PurchasingInvoiceSparepartDetail = new HashSet<PurchasingInvoiceSparepartDetail>();
            this.PackingList = new HashSet<PackingList>();
            this.PurchasingInvoiceSampleDetail = new HashSet<PurchasingInvoiceSampleDetail>();
        }
    
        public int PurchasingInvoiceID { get; set; }
        public string PurchasingInvoiceUD { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<int> BookingID { get; set; }
        public string Season { get; set; }
        public string FactoryInvoiceNo { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> InvoiceType { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public Nullable<int> FactoryCostTypeID { get; set; }
        public Nullable<bool> IsExpotedToExact { get; set; }
        public Nullable<bool> IsConfirmedPrice { get; set; }
        public Nullable<int> ConfirmedPriceBy { get; set; }
        public Nullable<System.DateTime> ConfirmedPriceDate { get; set; }
        public string Remark { get; set; }
        public Nullable<decimal> DepositPercent { get; set; }
        public Nullable<decimal> DepositAmount { get; set; }
        public Nullable<decimal> DepositFactoryAmount { get; set; }
        public Nullable<decimal> DepositAmountSparepart { get; set; }
        public Nullable<decimal> DepositFactoryAmountSparepart { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchasingInvoiceDetail> PurchasingInvoiceDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchasingInvoiceSparepartDetail> PurchasingInvoiceSparepartDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackingList> PackingList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchasingInvoiceSampleDetail> PurchasingInvoiceSampleDetail { get; set; }
    }
}
