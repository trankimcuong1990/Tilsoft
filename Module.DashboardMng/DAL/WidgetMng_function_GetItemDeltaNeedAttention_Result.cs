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
    
    public partial class WidgetMng_function_GetItemDeltaNeedAttention_Result
    {
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public Nullable<int> SaleOrderDetailID { get; set; }
        public Nullable<int> OfferSeasonDetailID { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string OfferUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public Nullable<int> PIStatus { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<decimal> OrderQnt40HC { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public string Currency { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<decimal> PurchasingPriceUSD { get; set; }
        public Nullable<decimal> LCCostUSD { get; set; }
        public Nullable<decimal> InterestUSD { get; set; }
        public Nullable<decimal> CommissionUSD { get; set; }
        public Nullable<decimal> DamagesCost { get; set; }
        public Nullable<decimal> TransportationUSD { get; set; }
        public Nullable<decimal> Delta5 { get; set; }
        public Nullable<decimal> Delta5Percent { get; set; }
        public string Season { get; set; }
        public int OfferSeasonID { get; set; }
        public int OfferID { get; set; }
        public int ClientID { get; set; }
    }
}