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
    
    public partial class SupportMng_FactoryTeamStep_View
    {
        public int FactoryTeamStepID { get; set; }
        public Nullable<int> FactoryTeamID { get; set; }
        public Nullable<int> FactoryStepID { get; set; }
        public Nullable<int> StepIndex { get; set; }
        public string FactoryStepNM { get; set; }
    
        public virtual SupportMng_FactoryTeam_View SupportMng_FactoryTeam_View { get; set; }
    }
}
