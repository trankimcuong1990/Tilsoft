//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.CushionColorMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CushionColorProductGroup
    {
        public int CushionColorProductGroupID { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public Nullable<int> ProductGroupID { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
    
        public virtual CushionColor CushionColor { get; set; }
    }
}
