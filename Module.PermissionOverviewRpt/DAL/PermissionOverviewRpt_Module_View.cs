//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PermissionOverviewRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PermissionOverviewRpt_Module_View
    {
        public int ModuleID { get; set; }
        public string DisplayText { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public int ParentID { get; set; }
        public Nullable<int> CreateCount { get; set; }
        public Nullable<int> ReadCount { get; set; }
        public Nullable<int> UpdateCount { get; set; }
        public Nullable<int> DeleteCount { get; set; }
        public Nullable<int> PrintCount { get; set; }
        public Nullable<int> ApproveCount { get; set; }
        public Nullable<int> ResetCount { get; set; }
    }
}
