//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ClientCityMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientCityMng_ClientCity_View
    {
        public int ClientCityID { get; set; }
        public string ClientCityUD { get; set; }
        public string ClientCityNM { get; set; }
        public Nullable<int> ClientCountryID { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string CreatorName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string ClientCountryNM { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    }
}
