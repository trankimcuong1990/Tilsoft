//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.WarehouseImportMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class WarehouseImportSparepartDetail
    {
        public WarehouseImportSparepartDetail()
        {
            this.WarehouseImportAreaDetail = new HashSet<WarehouseImportAreaDetail>();
        }
    
        public int WarehouseImportSparepartDetailID { get; set; }
        public Nullable<int> SparepartID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> ProductStatusID { get; set; }
        public Nullable<int> ECommercialInvoiceSparepartDetailID { get; set; }
        public Nullable<int> LoadingPlanSparepartDetailID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<int> LoadingPlanID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> RefQnt { get; set; }
        public Nullable<int> WexQnt { get; set; }
    
        public virtual WarehouseImport WarehouseImport { get; set; }
        public virtual ICollection<WarehouseImportAreaDetail> WarehouseImportAreaDetail { get; set; }
    }
}
