//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.SaleOrderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class SaleOrderProductReturn
    {
        public int SaleOrderProductReturnID { get; set; }
        public Nullable<int> LoadingPlanDetailID { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<int> ReturnIndex { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual SaleOrderDetail_Return SaleOrderDetail { get; set; }
    }
}
