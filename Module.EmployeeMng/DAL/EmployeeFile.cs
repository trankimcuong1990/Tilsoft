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
    
    public partial class EmployeeFile
    {
        public int EmployeeFileID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
