//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryGoodsProcedure.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryGoodsProcedure
    {
        public FactoryGoodsProcedure()
        {
            this.FactoryGoodsProcedureDetail = new HashSet<FactoryGoodsProcedureDetail>();
        }
    
        public int FactoryGoodsProcedureID { get; set; }
        public string FactoryGoodsProcedureUD { get; set; }
        public string FactoryGoodsProcedureNM { get; set; }
    
        public virtual ICollection<FactoryGoodsProcedureDetail> FactoryGoodsProcedureDetail { get; set; }
    }
}
