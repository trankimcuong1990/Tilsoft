//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchaseOrderMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseOrderMng_PurchaseOrderDetail_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseOrderMng_PurchaseOrderDetail_View()
        {
            this.PurchaseOrderMng_PurchaseOrderWorkOrderDetail_View = new HashSet<PurchaseOrderMng_PurchaseOrderWorkOrderDetail_View>();
            this.PurchaseOrderMng_PurchaseOrderDetailReceivingPlan_View = new HashSet<PurchaseOrderMng_PurchaseOrderDetailReceivingPlan_View>();
            this.PurchaseOrderMng_PurchaseOrderDetailPriceHistory_View = new HashSet<PurchaseOrderMng_PurchaseOrderDetailPriceHistory_View>();
        }
    
        public int PurchaseOrderDetailID { get; set; }
        public Nullable<int> PurchaseOrderID { get; set; }
        public Nullable<int> PurchaseRequestDetailID { get; set; }
        public Nullable<decimal> OrderQnt { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<System.DateTime> ETA { get; set; }
        public string Remark { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public Nullable<decimal> StockQnt { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<decimal> RequestQnt { get; set; }
        public Nullable<int> PurchaseQuotationDetailID { get; set; }
        public string UnitNM { get; set; }
        public Nullable<decimal> OldPrice { get; set; }
        public Nullable<bool> IsChangePrice { get; set; }
        public Nullable<decimal> TotalReceived { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string ID { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public Nullable<int> MaterialHistoryID { get; set; }
        public Nullable<int> MaterialsPriceID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrderMng_PurchaseOrderWorkOrderDetail_View> PurchaseOrderMng_PurchaseOrderWorkOrderDetail_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrderMng_PurchaseOrderDetailReceivingPlan_View> PurchaseOrderMng_PurchaseOrderDetailReceivingPlan_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrderMng_PurchaseOrderDetailPriceHistory_View> PurchaseOrderMng_PurchaseOrderDetailPriceHistory_View { get; set; }
        public virtual PurchaseOrderMng_PurchaseOrder_View PurchaseOrderMng_PurchaseOrder_View { get; set; }
    }
}
