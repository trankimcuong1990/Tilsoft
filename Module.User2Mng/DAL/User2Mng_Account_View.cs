//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.User2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class User2Mng_Account_View
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<bool> IsActivated { get; set; }
    }
}