//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ClientComplaint.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientComplaint_FactoryOrderDetailItemsByClientSeason_SearchView
    {
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string FactoryOrderUD { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public string Season { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<bool> IsShipped { get; set; }
        public Nullable<bool> IsLoaded { get; set; }
        public int LoadingPlanDetailID { get; set; }
        public Nullable<decimal> OriginalSellingPrice { get; set; }
        public Nullable<decimal> TotalSaleValue { get; set; }
    }
}