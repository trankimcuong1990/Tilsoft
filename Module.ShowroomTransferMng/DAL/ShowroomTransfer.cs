//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ShowroomTransferMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShowroomTransfer
    {
        public int ShowroomTransferID { get; set; }
        public string ShowroomTransferUD { get; set; }
        public Nullable<System.DateTime> ShowroomTransferDate { get; set; }
        public Nullable<int> FromWarehouseID { get; set; }
        public Nullable<int> ToWarehouseID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual ShowroomTransfer ShowroomTransfer1 { get; set; }
        public virtual ShowroomTransfer ShowroomTransfer2 { get; set; }
    }
}
