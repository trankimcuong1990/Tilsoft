//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.DefectsMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class DefectsMng_Defects_SearchView
    {
        public int DefectID { get; set; }
        public string DefectUD { get; set; }
        public string DefectNM { get; set; }
        public Nullable<int> DefectA { get; set; }
        public Nullable<int> DefectB { get; set; }
        public Nullable<int> DefectC { get; set; }
        public Nullable<int> DefectGroupID { get; set; }
        public string DefectGroupNM { get; set; }
        public string TypeOfDefectANM { get; set; }
        public string TypeOfDefectBNM { get; set; }
        public string TypeOfDefectCNM { get; set; }
        public string DefectViNM { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
    }
}
