//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ReceiptNoteMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReceiptNoteMng_ReceiptNoteOtherEdit_View
    {
        public int ReceiptNoteOtherID { get; set; }
        public Nullable<int> ReceiptNoteID { get; set; }
        public Nullable<int> NoteItemID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Remark { get; set; }
        public string EmployeeUD { get; set; }
        public string EmployeeNM { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
    
        public virtual ReceiptNoteMng_ReceiptNoteEdit_View ReceiptNoteMng_ReceiptNoteEdit_View { get; set; }
    }
}