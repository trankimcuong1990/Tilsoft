//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.FactoryOrderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryOrderStatus
    {
        public int FactoryOrderStatusID { get; set; }
        public Nullable<int> FactoryOrderID { get; set; }
        public Nullable<int> TrackingStatusID { get; set; }
        public Nullable<System.DateTime> StatusDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<bool> IsCurrentStatus { get; set; }
    
        public virtual FactoryOrder FactoryOrder { get; set; }
    }
}