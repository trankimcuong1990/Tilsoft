//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.BackCushionMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class BackCushionProductGroup
    {
        public int BackCushionProductGroupID { get; set; }
        public Nullable<int> BackCushionID { get; set; }
        public Nullable<int> ProductGroupID { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
    
        public virtual BackCushion BackCushion { get; set; }
    }
}
