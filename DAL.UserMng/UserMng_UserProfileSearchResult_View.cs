//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.UserMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserMng_UserProfileSearchResult_View
    {
        public int UserId { get; set; }
        public string UserUD { get; set; }
        public string FullName { get; set; }
        public string UserGroupNM { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string SkypeID { get; set; }
        public string OfficeNM { get; set; }
        public string PersonalPhoto_DisplayURL { get; set; }
        public Nullable<bool> IsActivated { get; set; }
        public string CreatorName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<int> UserGroupID { get; set; }
        public Nullable<int> OfficeID { get; set; }
    }
}
