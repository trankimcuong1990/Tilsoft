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
    
    public partial class SaleOrder_Return
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SaleOrder_Return()
        {
            this.SaleOrderStatus = new HashSet<SaleOrderStatus_Return>();
            this.SaleOrderDetail = new HashSet<SaleOrderDetail_Return>();
            this.SaleOrderDetailSparepart = new HashSet<SaleOrderDetailSparepart_Return>();
            this.WarehouseImportSparepartDetail = new HashSet<WarehouseImportSparepartDetail>();
            this.WarehouseImportProductDetail = new HashSet<WarehouseImportProductDetail>();
        }
    
        public int SaleOrderID { get; set; }
        public Nullable<int> OfferID { get; set; }
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
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public string DeleteRemark { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string OrderType { get; set; }
        public Nullable<int> V4ID { get; set; }
        public string V4PINo { get; set; }
        public Nullable<int> PaymentStatusID { get; set; }
        public string PaymentRemark { get; set; }
        public Nullable<int> OriginSaleOrderID { get; set; }
        public Nullable<int> Quantity20DC { get; set; }
        public Nullable<int> Quantity40DC { get; set; }
        public Nullable<int> Quantity40HC { get; set; }
        public Nullable<bool> IsViewDeliveryDateOnPrintout { get; set; }
        public Nullable<bool> IsViewQuantityContOnPrintout { get; set; }
        public Nullable<bool> IsViewLDSOnPrintout { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<int> PaymentUpdatedBy { get; set; }
        public Nullable<System.DateTime> PaymentUpdatedDate { get; set; }
        public string SignedPIFile { get; set; }
        public string ClientPOFile { get; set; }
        public Nullable<int> LessThanContainerLoad { get; set; }
        public string ClientOrderNumber { get; set; }
        public Nullable<int> ClientID { get; set; }
    
        public virtual Offer_Return Offer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleOrderStatus_Return> SaleOrderStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleOrderDetail_Return> SaleOrderDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleOrderDetailSparepart_Return> SaleOrderDetailSparepart { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarehouseImportSparepartDetail> WarehouseImportSparepartDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarehouseImportProductDetail> WarehouseImportProductDetail { get; set; }
    }
}
