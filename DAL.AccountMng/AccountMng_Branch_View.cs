//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.AccountMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccountMng_Branch_View
    {
        public int BranchID { get; set; }
        public string BranchUD { get; set; }
        public string BranchNM { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual AccountMng_UserProfile_View AccountMng_UserProfile_View { get; set; }
    }
}
