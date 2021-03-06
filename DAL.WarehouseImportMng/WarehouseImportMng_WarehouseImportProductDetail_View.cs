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
    
    public partial class WarehouseImportMng_WarehouseImportProductDetail_View
    {
        public WarehouseImportMng_WarehouseImportProductDetail_View()
        {
            this.WarehouseImportMng_WarehouseImportAreaDetail_View = new HashSet<WarehouseImportMng_WarehouseImportAreaDetail_View>();
            this.WarehouseImportMng_WarehouseImportColliDetail_View = new HashSet<WarehouseImportMng_WarehouseImportColliDetail_View>();
        }
    
        public int WarehouseImportProductDetailID { get; set; }
        public Nullable<int> WarehouseImportID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> ProductStatusID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> ECommercialInvoiceDetailID { get; set; }
        public Nullable<int> LoadingPlanDetailID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ProductStatusNM { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string FactoryUD { get; set; }
        public string BLNo { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerTypeNM { get; set; }
        public string SealNo { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> RefQnt { get; set; }
        public Nullable<int> WexQnt { get; set; }
        public Nullable<int> LoadingPlanID { get; set; }
    
        public virtual WarehouseImportMng_WarehouseImport_View WarehouseImportMng_WarehouseImport_View { get; set; }
        public virtual ICollection<WarehouseImportMng_WarehouseImportAreaDetail_View> WarehouseImportMng_WarehouseImportAreaDetail_View { get; set; }
        public virtual ICollection<WarehouseImportMng_WarehouseImportColliDetail_View> WarehouseImportMng_WarehouseImportColliDetail_View { get; set; }
    }
}
