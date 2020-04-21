//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchasingPriceOverview2Rpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchasingPriceOverview2Rpt_HTMLReportData_View
    {
        public long PrimaryID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string VNMonitorName { get; set; }
        public string NLMonitorName { get; set; }
        public string AgentNM { get; set; }
        public string SaleNM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Qnt40HC { get; set; }
        public int OrderQnt { get; set; }
        public Nullable<decimal> OrderQnt40HC { get; set; }
        public int LoadedQnt { get; set; }
        public Nullable<decimal> LoadedQnt40HC { get; set; }
        public int ShippedQnt { get; set; }
        public Nullable<decimal> ShippedQnt40HC { get; set; }
        public Nullable<int> ToBeShippedQnt { get; set; }
        public Nullable<decimal> ToBeShippedQnt40HC { get; set; }
        public string MaterialNM { get; set; }
        public string ProductionStatus { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public string SC { get; set; }
        public string QuotationUD { get; set; }
        public string ManufacturerCountryNM { get; set; }
        public string City { get; set; }
        public string PriceDifferenceCode { get; set; }
        public Nullable<decimal> MinSalePriceInThePast2Season { get; set; }
        public Nullable<decimal> MaxSalePriceInThePast2Season { get; set; }
        public Nullable<decimal> MinSalePriceInTheLastSeason { get; set; }
        public Nullable<decimal> MaxSalePriceInTheLastSeason { get; set; }
        public Nullable<decimal> PurchasingInvoicePrice { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public Nullable<decimal> TargetPrice { get; set; }
        public Nullable<decimal> BreakDownPrice { get; set; }
        public string QuotationStatusNM { get; set; }
        public Nullable<System.DateTime> StatusUpdatedDate { get; set; }
        public string FactoryProformaInvoiceNo { get; set; }
        public string Remark { get; set; }
        public string Season { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public int ItemType { get; set; }
    }
}
