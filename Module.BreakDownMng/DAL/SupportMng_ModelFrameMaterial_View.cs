//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.BreakDownMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupportMng_ModelFrameMaterial_View
    {
        public long PrimaryID { get; set; }
        public int FrameMaterialID { get; set; }
        public string FrameMaterialUD { get; set; }
        public string FrameMaterialNM { get; set; }
        public Nullable<int> MaterialOptionID { get; set; }
        public string Season { get; set; }
        public Nullable<bool> IsStandard { get; set; }
        public Nullable<int> ProductGroupID { get; set; }
        public Nullable<int> DisplayIndex { get; set; }
    }
}
