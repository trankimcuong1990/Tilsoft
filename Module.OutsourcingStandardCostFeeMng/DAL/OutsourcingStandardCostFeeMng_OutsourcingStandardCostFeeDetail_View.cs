//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.OutsourcingStandardCostFeeMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class OutsourcingStandardCostFeeMng_OutsourcingStandardCostFeeDetail_View
    {
        public int OutsourcingStandardCostFeeID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> OutsourcingCostID { get; set; }
        public Nullable<int> OursourcingCostTypeID { get; set; }
        public Nullable<decimal> StandardPrice { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
        public string ModelUD { get; set; }
        public long KeyID { get; set; }
    
        public virtual OutsourcingStandardCostFeeMng_Model_SearchView OutsourcingStandardCostFeeMng_Model_SearchView { get; set; }
        public virtual OutsourcingStandardCostFeeMng_ModelPiece_SearchView OutsourcingStandardCostFeeMng_ModelPiece_SearchView { get; set; }
    }
}