//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Support.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupportMng_Model_View
    {
        public int ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public Nullable<int> ProductGroupID { get; set; }
        public bool HasCushion { get; set; }
        public bool HasFrameMaterial { get; set; }
        public bool HasSubMaterial { get; set; }
        public string ManufacturerCountryUD { get; set; }
        public Nullable<int> ManufacturerCountryID { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string ImageFile_FullSizeUrl { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
    }
}
