//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ClientComplaint.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientComplaint_ClientComplaintUser_View
    {
        public int ClientComplaintUserId { get; set; }
        public int ClientComplaintId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeNM { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual ClientComplaint_ClientComplaint_View ClientComplaint_ClientComplaint_View { get; set; }
    }
}