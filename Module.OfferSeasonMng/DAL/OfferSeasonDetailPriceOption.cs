//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.OfferSeasonMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfferSeasonDetailPriceOption
    {
        public int OfferSeasonDetailPriceOptionID { get; set; }
        public Nullable<int> ProductElementID { get; set; }
        public Nullable<int> ModelPriceConfigurationDetailID { get; set; }
        public Nullable<decimal> IncreasePercent { get; set; }
        public Nullable<decimal> IncreaseAmount { get; set; }
    
        public virtual OfferSeasonDetail OfferSeasonDetail { get; set; }
    }
}