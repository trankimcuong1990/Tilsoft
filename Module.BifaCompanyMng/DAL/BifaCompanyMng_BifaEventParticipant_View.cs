//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.BifaCompanyMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class BifaCompanyMng_BifaEventParticipant_View
    {
        public int BifaEventParticipantID { get; set; }
        public Nullable<int> BifaCompanyID { get; set; }
        public Nullable<int> BifaEventID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
    
        public virtual BifaCompanyMng_BifaCompany_View BifaCompanyMng_BifaCompany_View { get; set; }
        public virtual BifaCompanyMng_BifaEvent_View BifaCompanyMng_BifaEvent_View { get; set; }
    }
}