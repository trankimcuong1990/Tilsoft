//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductionFrameWeightMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductionFrameWeightMng_Client_View
    {
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public long RowIndex { get; set; }
    
        public virtual ProductionFrameWeightMng_ProductionFrameWeightSearchResult_View ProductionFrameWeightMng_ProductionFrameWeightSearchResult_View { get; set; }
    }
}
