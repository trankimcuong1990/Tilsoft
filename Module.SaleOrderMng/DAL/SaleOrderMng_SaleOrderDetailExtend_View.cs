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
    
    public partial class SaleOrderMng_SaleOrderDetailExtend_View
    {
        public int SaleOrderDetailExtendID { get; set; }
        public Nullable<int> SaleOrderDetailID { get; set; }
        public string Description { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public Nullable<int> V4ID { get; set; }
    
        public virtual SaleOrderMng_SaleOrderDetail_View SaleOrderMng_SaleOrderDetail_View { get; set; }
    }
}
