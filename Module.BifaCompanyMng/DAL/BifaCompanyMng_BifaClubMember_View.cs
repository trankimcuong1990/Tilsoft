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
    
    public partial class BifaCompanyMng_BifaClubMember_View
    {
        public int BifaClubMemberID { get; set; }
        public Nullable<int> BifaClubID { get; set; }
        public Nullable<int> BifaCompanyID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string BifaClubUD { get; set; }
        public string BifaClubNM { get; set; }
        public Nullable<System.DateTime> JoinedDate { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
    
        public virtual BifaCompanyMng_BifaCompany_View BifaCompanyMng_BifaCompany_View { get; set; }
    }
}