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
    
    public partial class SupportMng_ModelSubMaterial_View
    {
        public long PrimaryID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> SubMaterialID { get; set; }
        public string SubMaterialUD { get; set; }
        public string SubMaterialNM { get; set; }
        public string Season { get; set; }
        public Nullable<bool> IsStandard { get; set; }
        public Nullable<int> DisplayIndex { get; set; }
    }
}
