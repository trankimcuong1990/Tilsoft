//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.EmployeeMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class QAQCFactoryAccess
    {
        public int QAQCFactoryAccessID { get; set; }
        public Nullable<int> UserAccessID { get; set; }
        public Nullable<int> ApprovalID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<int> FactoryID { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
