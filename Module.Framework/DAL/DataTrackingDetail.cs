//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Framework.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class DataTrackingDetail
    {
        public int DataTrackingDetailID { get; set; }
        public string ColumnName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    
        public virtual DataTrackingAction DataTrackingAction { get; set; }
    }
}
