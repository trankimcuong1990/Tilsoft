//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductionFrameWeightMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductionFrameWeightMng_ProductionFrameWeightHistory_View
    {
        public int ProductionFrameWeightHistoryID { get; set; }
        public Nullable<int> ProductionFrameWeightID { get; set; }
        public Nullable<decimal> FrameWeight { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
        public long RowIndex { get; set; }
    
        public virtual ProductionFrameWeightMng_ProductionFrameWeight_View ProductionFrameWeightMng_ProductionFrameWeight_View { get; set; }
    }
}
