//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.WarehouseTransportMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class WarehouseTransportMng_PhysicalStock_View
    {
        public long KeyID { get; set; }
        public Nullable<int> GoodsID { get; set; }
        public Nullable<int> ProductStatusID { get; set; }
        public string ProductType { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ProductStatusNM { get; set; }
        public Nullable<int> PhysicalQnt { get; set; }
    }
}
