//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Agents.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class AgentsMng_AmountByClientAndSeason_View
    {
        public string Season { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<decimal> AmountPI { get; set; }
        public Nullable<decimal> ComAmountPI { get; set; }
        public Nullable<decimal> ComPercentPI { get; set; }
        public Nullable<decimal> AmountCI { get; set; }
        public Nullable<decimal> ComAmountCI { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public Nullable<decimal> DefaultCommissionPercent { get; set; }
        public Nullable<decimal> Delta8Percent { get; set; }
        public Nullable<decimal> Delta8Amount { get; set; }
        public long KeyID { get; set; }
    }
}
