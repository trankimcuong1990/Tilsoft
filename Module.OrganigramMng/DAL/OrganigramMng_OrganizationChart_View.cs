//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.OrganigramMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrganigramMng_OrganizationChart_View
    {
        public int OrganigramID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string CompanyUD { get; set; }
        public string CompanyNM { get; set; }
        public string Address { get; set; }
        public string ImageFile { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FriendlyName { get; set; }
        public string WorkSpaceFile { get; set; }
        public string WorkSpaceFileThumbnail { get; set; }
        public string WorkSpaceFileLocation { get; set; }
    }
}