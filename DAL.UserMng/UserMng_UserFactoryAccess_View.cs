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
    
    public partial class UserMng_UserFactoryAccess_View
    {
        public int UserFactoryAccessID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<bool> CanAccess { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public Nullable<bool> CanReceiveNotification { get; set; }
    
        public virtual UserMng_UserProfile_View UserMng_UserProfile_View { get; set; }
    }
}
