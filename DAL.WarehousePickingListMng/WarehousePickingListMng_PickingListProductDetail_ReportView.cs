//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.WarehousePickingListMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class WarehousePickingListMng_PickingListProductDetail_ReportView
    {
        public int WarehousePickingListProductDetailID { get; set; }
        public Nullable<int> WarehousePickingListID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string WarehouseAreaUD { get; set; }
        public string Barcode { get; set; }
        public string ImageFile { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Colli { get; set; }
        public string PackagingMethodNM { get; set; }
        public Nullable<int> PlaningQuantity { get; set; }
        public Nullable<decimal> PlanningIn40HC { get; set; }
        public string ItemPickingStatus { get; set; }
        public string Remark { get; set; }
    
        public virtual WarehousePickingListMng_PickingList_ReportView WarehousePickingListMng_PickingList_ReportView { get; set; }
    }
}
