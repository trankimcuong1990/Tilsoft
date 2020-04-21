using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVTPurchasingPriceMng.DTO
{
    public class FactoryQuotationSearchResultDTO
    {
        public int QuotationDetailID { get; set; }
        public string Season { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string PriceDifferenceCode { get; set; }
        public string FactoryUD { get; set; }
        public decimal? PurchasingPrice3rdPreviousSeason { get; set; }
        public decimal? PurchasingPrice2ndPreviousSeason { get; set; }
        public decimal? PurchasingPricePreviousSeason { get; set; }
        public decimal? PurchasingPrice { get; set; }
        public decimal? PriceIncludeDiff { get; set; }
        public decimal? TargetPrice { get; set; }
        public string QuotationStatusNM { get; set; }
        public string StatusUpdatedDate { get; set; }
        public string StatusUpdatorNM { get; set; }
        public int? OrderQnt { get; set; }
        public decimal? Qnt40HC { get; set; }
        public decimal? OrderQnt40HC { get; set; }
        public string PackagingMethodNM { get; set; }
        public string Remark { get; set; }
        public string PriceUpdatedDate { get; set; }
        public decimal? PriceDifferenceRate { get; set; }
        public decimal? NewTargetPrice { get; set; }
        public string NewTargetComment { get; set; }
        public bool IsSelected { get; set; }

        public int? StatusUpdatedBy { get; set; }
        public int? StatusID { get; set; }
        public int? PackagingMethodID { get; set; }
        //public int? ClientID { get; set; }
        public int? FactoryID { get; set; }
        public int? ItemTypeID { get; set; }
        public int? ModelID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryOrderSparepartDetailID { get; set; }
        public int? QuotationOfferDirectionID { get; set; }
        public int? BOMStandardID { get; set; }
        public int? BOMID { get; set; }
        public int? OfferLineID { get; set; }
        public int? MaterialID { get; set; }
        public int? MaterialTypeID { get; set; }
        public int? MaterialColorID { get; set; }
        public int? FrameMaterialID { get; set; }
        public int? FrameMaterialColorID { get; set; }
        public int? SubMaterialID { get; set; }
        public int? SubMaterialColorID { get; set; }
        public int? BackCushionID { get; set; }
        public int? SeatCushionID { get; set; }
        public int? CushionColorID { get; set; }
        public int? FSCTypeID { get; set; }
        public int? FSCPercentID { get; set; }
        public int? ClientSpecialPackagingMethodID { get; set; }
        public string SpecialRequirement { get; set; }
        public string LDS { get; set; }
        public string DeliveryTerm { get; set; }
        public decimal? ExportCost40HC { get; set; }
        public string ThumbnailLocation { get; set;}
        public string FileLocation { get; set; }
        public decimal? AVFBreakdownAmount { get; set; }
        public decimal? AVTBreakdownAmount { get; set; }
        public int? AVFBreakdownLoadability { get; set; }
        public int? AVTBreakdownLoadability { get; set; }
        public int TotalShippedQnt { get; set; }
        public string PackagingType { get; set; }
    }
}
