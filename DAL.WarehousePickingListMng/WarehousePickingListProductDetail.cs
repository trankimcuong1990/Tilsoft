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
    
    public partial class WarehousePickingListProductDetail
    {
        public WarehousePickingListProductDetail()
        {
            this.WarehousePickingListAreaDetail = new HashSet<WarehousePickingListAreaDetail>();
        }
    
        public int WarehousePickingListProductDetailID { get; set; }
        public Nullable<int> WarehousePickingListID { get; set; }
        public Nullable<int> SaleOrderDetailID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> ProductStatusID { get; set; }
        public Nullable<int> PlaningQuantity { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> WarehouseAreaID { get; set; }
        public string Unit { get; set; }
        public string Colli { get; set; }
        public Nullable<int> PackagingMethodID { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsChecked { get; set; }
    
        public virtual WarehousePickingList WarehousePickingList { get; set; }
        public virtual ICollection<WarehousePickingListAreaDetail> WarehousePickingListAreaDetail { get; set; }
    }
}
