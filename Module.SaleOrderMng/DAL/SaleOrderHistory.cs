//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.SaleOrderMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SaleOrderHistory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SaleOrderHistory()
        {
            this.SaleOrderHistoryDetail = new HashSet<SaleOrderHistoryDetail>();
        }
    
        public int SaleOrderHistoryID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public string Season { get; set; }
        public Nullable<int> SaleOrderVersion { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsPIReceived { get; set; }
        public string PIReceivedBy { get; set; }
        public Nullable<System.DateTime> PIReceivedDate { get; set; }
        public string PIReceivedRemark { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<System.DateTime> DeliveryDateInternal { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public string Reference { get; set; }
        public string Conditions { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Transportation { get; set; }
        public Nullable<decimal> CommissionPercent { get; set; }
        public Nullable<decimal> Commission { get; set; }
        public Nullable<decimal> VATPercent { get; set; }
        public Nullable<int> PaymentTermID { get; set; }
        public Nullable<int> DeliveryTermID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleOrderHistoryDetail> SaleOrderHistoryDetail { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
    }
}
