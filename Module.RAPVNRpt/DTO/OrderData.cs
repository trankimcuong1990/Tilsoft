using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.RAPVNRpt.DTO
{
    public class OrderData
    {
        public string Season { get; set; }
        public int? FactoryID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientOrderNumber { get; set; }
        public string FactoryOrderDate { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string SaleNM { get; set; }
        public string Sale2NM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Qnt40HC { get; set; }
        public int? OrderQnt { get; set; }
        public decimal? OrderQntIn40HC { get; set; }
        public int TotalShippedQnt { get; set; }
        public decimal? TotalShippedQntIn40HC { get; set; }
        public int? ToBeShippedQnt { get; set; }
        public decimal? ToBeShippedQntIn40HC { get; set; }
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
        public int? Quantity20DC { get; set; }
        public int? Quantity40DC { get; set; }
        public int? Quantity40HC { get; set; }
        public string LDSClient { get; set; }
        public string DeliveryDate { get; set; }
        public string LDSFacory { get; set; }
        public string RangeName { get; set; }
        public string FrameMaterialNM { get; set; }
        public string MaterialTypeNM { get; set; }
        public string MaterialColorNM { get; set; }
        public string CushionColorNM { get; set; }
        public long RowIndex { get; set; }
        public int? ProductionQnt { get; set; }
        public int? RemainQnt { get; set; }
        public string Company { get; set; }
    }
}
