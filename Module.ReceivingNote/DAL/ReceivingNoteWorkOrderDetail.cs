//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ReceivingNote.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReceivingNoteWorkOrderDetail
    {
        public int ReceivingNoteWorkOrderDetailID { get; set; }
        public Nullable<int> ReceivingNoteDetailID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public Nullable<decimal> ReceivingQnt { get; set; }
    
        public virtual WorkOrder WorkOrder { get; set; }
        public virtual ReceivingNoteDetail ReceivingNoteDetail { get; set; }
    }
}
