//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.Support
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupportMng_ModelMaterialType_View
    {
        public long PrimaryID { get; set; }
        public int MaterialTypeID { get; set; }
        public string MaterialTypeUD { get; set; }
        public string MaterialTypeNM { get; set; }
        public string Season { get; set; }
        public Nullable<bool> IsStandard { get; set; }
        public Nullable<int> ProductGroupID { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public Nullable<int> DisplayIndex { get; set; }
    }
}
