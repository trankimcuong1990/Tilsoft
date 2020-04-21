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
    
    public partial class WarehouseImportProductDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WarehouseImportProductDetail()
        {
            this.WarehouseImportAreaDetail = new HashSet<WarehouseImportAreaDetail>();
        }
    
        public int WarehouseImportProductDetailID { get; set; }
        public Nullable<int> WarehouseImportID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> ProductStatusID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> ECommercialInvoiceDetailID { get; set; }
        public Nullable<int> LoadingPlanDetailID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<int> LoadingPlanID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> RefQnt { get; set; }
        public Nullable<int> WexQnt { get; set; }
    
        public virtual WarehouseImport WarehouseImport { get; set; }
        public virtual SaleOrder_Return SaleOrder { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarehouseImportAreaDetail> WarehouseImportAreaDetail { get; set; }
    }
}