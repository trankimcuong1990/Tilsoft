//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryLocationMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Location
    {
        public int LocationID { get; set; }
        public string LocationNM { get; set; }
        public Nullable<int> ManufacturerCountryID { get; set; }
        public string LocationUD { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
    }
}
