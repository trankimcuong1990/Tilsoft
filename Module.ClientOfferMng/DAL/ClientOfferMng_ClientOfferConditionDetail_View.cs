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
    
    public partial class ClientOfferMng_ClientOfferConditionDetail_View
    {
        public int ClientOfferConditionDetailID { get; set; }
        public Nullable<int> ClientOfferID { get; set; }
        public string Remark { get; set; }
        public string Condition20DC { get; set; }
        public string Condition40DC { get; set; }
        public string Condition40HC { get; set; }
        public Nullable<int> POLID { get; set; }
        public Nullable<int> PODID { get; set; }
        public Nullable<int> ClientConditionItemID { get; set; }
        public string ClientConditionItemNM { get; set; }
    
        public virtual ClientOfferMng_ClientOffer_View ClientOfferMng_ClientOffer_View { get; set; }
    }
}
