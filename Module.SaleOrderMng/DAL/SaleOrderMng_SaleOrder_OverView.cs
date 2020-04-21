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
    
    public partial class SaleOrderMng_SaleOrder_OverView
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SaleOrderMng_SaleOrder_OverView()
        {
            this.SaleOrderMng_SaleOrderDetail_OverView = new HashSet<SaleOrderMng_SaleOrderDetail_OverView>();
            this.SaleOrderMng_SaleOrderExtend_OverView = new HashSet<SaleOrderMng_SaleOrderExtend_OverView>();
            this.SaleOrderMng_SaleOrderDetailSparepart_OverView = new HashSet<SaleOrderMng_SaleOrderDetailSparepart_OverView>();
            this.SaleOrderMng_WarehouseImport_OverView = new HashSet<SaleOrderMng_WarehouseImport_OverView>();
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
        public byte[] ConcurrencyFlag { get; set; }
        public string OrderType { get; set; }
        public Nullable<int> V4ID { get; set; }
        public string V4PINo { get; set; }
        public Nullable<int> OriginSaleOrderID { get; set; }
        public Nullable<int> Quantity20DC { get; set; }
        public Nullable<int> Quantity40DC { get; set; }
        public Nullable<int> Quantity40HC { get; set; }
        public Nullable<bool> IsViewQuantityContOnPrintout { get; set; }
        public Nullable<bool> IsViewDeliveryDateOnPrintout { get; set; }
        public Nullable<bool> IsViewLDSOnPrintout { get; set; }
        public Nullable<int> PaymentStatusID { get; set; }
        public string PaymentRemark { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string SignedPIFile { get; set; }
        public string ClientPOFile { get; set; }
        public Nullable<int> LessThanContainerLoad { get; set; }
        public string ClientOrderNumber { get; set; }
        public string TrackingStatusNM { get; set; }
        public string PaymentTermNM { get; set; }
        public string PaymentTypeNM { get; set; }
        public string DeliveryTermNM { get; set; }
        public string DeliveryTypeNM { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string CreatorName2 { get; set; }
        public string UpdatorName2 { get; set; }
        public string PIReceiver { get; set; }
        public string OfferUD { get; set; }
        public Nullable<int> TrackingStatusID { get; set; }
        public string PaymentStatusNM { get; set; }
        public Nullable<bool> IsDPReceived { get; set; }
        public Nullable<bool> IsLCReceived { get; set; }
        public Nullable<bool> IsNAReceived { get; set; }
        public string Currency { get; set; }
        public Nullable<int> ClientPaymentID { get; set; }
        public string SignedPIFileURL { get; set; }
        public string SignedPIFileFriendlyName { get; set; }
        public string ClientPOFileURL { get; set; }
        public string ClientPOFriendlyName { get; set; }
        public string DefaultPiReport { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleOrderMng_SaleOrderDetail_OverView> SaleOrderMng_SaleOrderDetail_OverView { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleOrderMng_SaleOrderExtend_OverView> SaleOrderMng_SaleOrderExtend_OverView { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleOrderMng_SaleOrderDetailSparepart_OverView> SaleOrderMng_SaleOrderDetailSparepart_OverView { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleOrderMng_WarehouseImport_OverView> SaleOrderMng_WarehouseImport_OverView { get; set; }
    }
}
