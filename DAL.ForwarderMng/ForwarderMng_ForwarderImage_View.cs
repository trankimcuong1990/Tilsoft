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
    
    public partial class ForwarderMng_ForwarderImage_View
    {
        public int ForwarderImageID { get; set; }
        public Nullable<int> ForwarderID { get; set; }
        public string FileUD { get; set; }
        public string Remark { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string ContactPersonNM { get; set; }
        public string JobTitle { get; set; }
        public Nullable<int> InternalDepartmentID { get; set; }
        public string InternalDepartmentNM { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
    
        public virtual ForwarderMng_Forwarder_View ForwarderMng_Forwarder_View { get; set; }
    }
}
