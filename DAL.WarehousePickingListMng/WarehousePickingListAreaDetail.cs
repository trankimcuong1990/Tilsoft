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
    
    public partial class WarehousePickingListAreaDetail
    {
        public int WarehousePickingListAreaDetailID { get; set; }
        public Nullable<int> WarehouseAreaID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Remark { get; set; }
    
        public virtual WarehousePickingListProductDetail WarehousePickingListProductDetail { get; set; }
        public virtual WarehousePickingListSparepartDetail WarehousePickingListSparepartDetail { get; set; }
    }
}
