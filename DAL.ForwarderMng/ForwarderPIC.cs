//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ForwarderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ForwarderPIC
    {
        public int ForwarderPICID { get; set; }
        public Nullable<int> ForwarderID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
    
        public virtual Forwarder Forwarder { get; set; }
    }
}