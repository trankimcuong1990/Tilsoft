//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PriceListForwarder.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PriceListForwarder_PriceListForwarderDetail_View
    {
        public int PriceListForwarderDetailID { get; set; }
        public Nullable<int> PoLID { get; set; }
        public Nullable<int> PoDID { get; set; }
        public Nullable<int> TypeOfContainerID { get; set; }
        public Nullable<decimal> PricePerUnit { get; set; }
        public Nullable<int> PriceListForwarderID { get; set; }
    
        public virtual PriceListForwarder_PriceListForwarder_View PriceListForwarder_PriceListForwarder_View { get; set; }
    }
}
