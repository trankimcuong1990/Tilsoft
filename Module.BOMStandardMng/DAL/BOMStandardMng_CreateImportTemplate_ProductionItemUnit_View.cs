//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.BOMStandardMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class BOMStandardMng_CreateImportTemplate_ProductionItemUnit_View
    {
        public long KeyID { get; set; }
        public Nullable<int> BOMStandardID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<decimal> ConversionFactor { get; set; }
        public string UnitNM { get; set; }
    
        public virtual BOMStandardMng_CreateImportTemplate_BOMStandard_View BOMStandardMng_CreateImportTemplate_BOMStandard_View { get; set; }
    }
}
