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
    
    public partial class ReceivingNoteMng_ReceivingNoteDetailSubUnit_View
    {
        public int ReceivingNoteDetailSubUnitID { get; set; }
        public Nullable<int> ReceivingNoteDetailID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string Remark { get; set; }
        public string UnitNM { get; set; }
    
        public virtual ReceivingNoteMng_ReceivingNoteDetail_View ReceivingNoteMng_ReceivingNoteDetail_View { get; set; }
    }
}
