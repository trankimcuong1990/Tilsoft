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
    
    public partial class ProductionPrice
    {
        public int ProductionPriceID { get; set; }
        public Nullable<int> ProductionItemTypeID { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<bool> IsLocked { get; set; }
        public Nullable<int> LockedBy { get; set; }
        public Nullable<System.DateTime> LockedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
