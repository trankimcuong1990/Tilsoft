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
    
    public partial class EmployeeMng_EmployeeFactory_View
    {
        public int EmployeeFactoryID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string FactoryUD { get; set; }
    
        public virtual EmployeeMng_Employee_View EmployeeMng_Employee_View { get; set; }
    }
}