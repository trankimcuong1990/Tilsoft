//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ForwarderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ForwarderImage
    {
        public int ForwarderImageID { get; set; }
        public Nullable<int> ForwarderID { get; set; }
        public string FileUD { get; set; }
        public string Remark { get; set; }
        public string ContactPersonNM { get; set; }
        public string JobTitle { get; set; }
        public Nullable<int> InternalDepartmentID { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
    
        public virtual Forwarder Forwarder { get; set; }
    }
}
