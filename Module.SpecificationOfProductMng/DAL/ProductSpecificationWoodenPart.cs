//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.SpecificationOfProductMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductSpecificationWoodenPart
    {
        public int ProductSpecificationWoodenPartID { get; set; }
        public Nullable<int> ProductSpecificationID { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public string DimensionL { get; set; }
        public string DimensionW { get; set; }
        public string DimensionH { get; set; }
        public string Weight { get; set; }
    
        public virtual ProductSpecification ProductSpecification { get; set; }
    }
}