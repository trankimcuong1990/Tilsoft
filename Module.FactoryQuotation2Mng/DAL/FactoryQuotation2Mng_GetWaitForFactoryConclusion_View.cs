//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryQuotation2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryQuotation2Mng_GetWaitForFactoryConclusion_View
    {
        public long RowNumber { get; set; }
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<int> TotalWaitedWeeks { get; set; }
        public Nullable<int> TotalItem { get; set; }
        public Nullable<int> TotalPendingItem { get; set; }
        public Nullable<int> PricingTeamMemberID { get; set; }
        public string PricingTeamMemberNM { get; set; }
    }
}