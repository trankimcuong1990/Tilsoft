//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PaymentNoteMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PaymentNoteMng_PaymentNoteSearch_View
    {
        public int PaymentNoteID { get; set; }
        public Nullable<int> PaymentNoteTypeID { get; set; }
        public string PaymentNoteNo { get; set; }
        public Nullable<System.DateTime> PaymentNoteDate { get; set; }
        public Nullable<System.DateTime> PostingDate { get; set; }
        public string Currency { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public Nullable<int> PaymentTypeID { get; set; }
        public string BankAcc { get; set; }
        public string ReceiverName { get; set; }
        public string AttachedFile { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string StatusNM { get; set; }
        public string Creator { get; set; }
        public string Updater { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string PaymentTypeNM { get; set; }
        public string PaymentNoteTypeNM { get; set; }
    }
}
