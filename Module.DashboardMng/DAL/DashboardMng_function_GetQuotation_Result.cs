//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.DashboardMng.DAL
{
    using System;
    
    public partial class DashboardMng_function_GetQuotation_Result
    {
        public int Id { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ClientUD { get; set; }
        public string ArticleCode { get; set; }
        public int OrderQnt { get; set; }
        public Nullable<decimal> AVTPrice { get; set; }
        public Nullable<decimal> FactoryPrice { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> PriceUpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string StatusName { get; set; }
        public Nullable<bool> HaveUrlLink { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string Season { get; set; }
        public int QuotationDetailID { get; set; }
        public Nullable<int> QuotationID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public int IsSetPrice { get; set; }
    }
}
