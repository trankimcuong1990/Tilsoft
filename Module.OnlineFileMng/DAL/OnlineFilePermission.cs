//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.OnlineFileMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class OnlineFilePermission
    {
        public int OnlineFilePermissionID { get; set; }
        public Nullable<int> OnlineFileID { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual OnlineFile OnlineFile { get; set; }
    }
}