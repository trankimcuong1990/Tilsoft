//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ClientOfferMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientOfferMng_ClientCostItem_View
    {
        public int ClientCostItemID { get; set; }
        public string ClientCostItemNM { get; set; }
        public Nullable<int> ClientCostTypeID { get; set; }
        public string Currency { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> ClientCostChargeTypeID { get; set; }
        public string UpdatorName { get; set; }
        public string ClientCostTypeNM { get; set; }
        public string ClientCostChargeTypeNM { get; set; }
        public Nullable<int> POLID { get; set; }
        public string PoLname { get; set; }
    }
}
