//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.RAPVNRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class RAPVNRpt_Order_View
    {
        public string Season { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientOrderNumber { get; set; }
        public string FactoryOrderDate { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string SaleNM { get; set; }
        public string Sale2NM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public int OrderQnt { get; set; }
        public Nullable<decimal> OrderQntIn40HC { get; set; }
        public int TotalShippedQnt { get; set; }
        public Nullable<decimal> TotalShippedQntIn40HC { get; set; }
        public Nullable<int> ToBeShippedQnt { get; set; }
        public Nullable<decimal> ToBeShippedQntIn40HC { get; set; }
        public string LocationNM { get; set; }
        public string FactoryUD { get; set; }
        public string SupplyChainPersonNM { get; set; }
        public string ProductionStatus { get; set; }
        public string OrderTypeNM { get; set; }
        public string Rating { get; set; }
        public string ItemStandardNM { get; set; }
        public string TestSamplingOptionNM { get; set; }
        public string LabelingOptionNM { get; set; }
        public string PackagingOptionNM { get; set; }
        public string InspectionOptionNM { get; set; }
        public string AdditionalRemark { get; set; }
        public string DeliveryTypeNM { get; set; }
        public string ShipmentToOptionNM { get; set; }
        public string ShipmentTypeOptionNM { get; set; }
        public Nullable<int> Quantity20DC { get; set; }
        public Nullable<int> Quantity40DC { get; set; }
        public Nullable<int> Quantity40HC { get; set; }
        public string LDSClient { get; set; }
        public string DeliveryDate { get; set; }
        public string LDSFacory { get; set; }
        public string RangeName { get; set; }
        public string FrameMaterialNM { get; set; }
        public string MaterialTypeNM { get; set; }
        public string MaterialColorNM { get; set; }
        public string CushionColorNM { get; set; }
        public long RowIndex { get; set; }
        public string Company { get; set; }
        public Nullable<int> ShippedByFactoryID { get; set; }
        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
    }
}
