//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ShowroomReceiptMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShowroomReceiptMng_ShowroomReceiptAreaDetail_View
    {
        public int ShowroomReceiptAreaDetailID { get; set; }
        public Nullable<int> ShowroomReceiptDetailID { get; set; }
        public Nullable<int> ShowroomAreaID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Remark { get; set; }
        public string ShowroomAreaUD { get; set; }
        public string ShowroomAreaNM { get; set; }
    
        public virtual ShowroomReceiptMng_ShowroomReceiptDetail_View ShowroomReceiptMng_ShowroomReceiptDetail_View { get; set; }
    }
}