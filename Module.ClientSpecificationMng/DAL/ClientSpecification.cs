//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ClientSpecificationMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientSpecification
    {
        public int ClientSpecificationID { get; set; }
        public string ClientSpecificationUD { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> OfferLineID { get; set; }
        public Nullable<int> ClientSpecificationTypeID { get; set; }
        public string ClientSpecificationFileUD { get; set; }
        public string ClientSpecificationRemark { get; set; }
        public Nullable<int> ClientSpecificationUpdatedBy { get; set; }
        public Nullable<System.DateTime> ClientSpecificationUpdatedDate { get; set; }
    }
}
