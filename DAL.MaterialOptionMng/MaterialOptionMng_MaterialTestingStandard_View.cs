//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.MaterialOptionMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class MaterialOptionMng_MaterialTestingStandard_View
    {
        public int MaterialTestUsingMaterialStandardID { get; set; }
        public Nullable<int> MaterialTestReportID { get; set; }
        public string TestStandardNM { get; set; }
    
        public virtual MaterialOptionMng_MaterialTesting_View MaterialOptionMng_MaterialTesting_View { get; set; }
    }
}
