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
    
    public partial class ReceiptNoteClient
    {
        public int ReceiptNoteClientID { get; set; }
        public Nullable<int> ReceiptNoteID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Remark { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
    
        public virtual ReceiptNote ReceiptNote { get; set; }
    }
}