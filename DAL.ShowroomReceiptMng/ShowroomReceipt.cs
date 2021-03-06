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
    
    public partial class ShowroomReceipt
    {
        public ShowroomReceipt()
        {
            this.ShowroomReceiptDetail = new HashSet<ShowroomReceiptDetail>();
        }
    
        public int ShowroomReceiptID { get; set; }
        public Nullable<int> ReceiptTypeID { get; set; }
        public string ReceiptNo { get; set; }
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public Nullable<int> ShowroomID { get; set; }
        public Nullable<int> StorekeeperID { get; set; }
        public string ImportFrom { get; set; }
        public string ExportTo { get; set; }
        public string Remark { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Season { get; set; }
    
        public virtual ICollection<ShowroomReceiptDetail> ShowroomReceiptDetail { get; set; }
    }
}
