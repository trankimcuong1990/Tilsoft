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
    
    public partial class WarehousePickingListMng_WarehousePickingListAreaDetail_View
    {
        public int WarehousePickingListAreaDetailID { get; set; }
        public Nullable<int> WarehousePickingListProductDetailID { get; set; }
        public Nullable<int> WarehousePickingListSparepartDetailID { get; set; }
        public Nullable<int> WarehouseAreaID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Remark { get; set; }
        public string WarehouseAreaUD { get; set; }
    
        public virtual WarehousePickingListMng_WarehousePickingListProductDetail_View WarehousePickingListMng_WarehousePickingListProductDetail_View { get; set; }
        public virtual WarehousePickingListMng_WarehousePickingListSparepartDetail_View WarehousePickingListMng_WarehousePickingListSparepartDetail_View { get; set; }
    }
}
