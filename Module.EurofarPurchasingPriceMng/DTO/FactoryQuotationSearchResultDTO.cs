using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarPurchasingPriceMng.DTO
{
    public class FactoryQuotationSearchResultDTO
    {
        public int? QuotationDetailID { get; set; }
        public string Season { get; set; }
        public int? ItemTypeID { get; set; }
        public string QuotationStatusNM { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string OfferSeasonUD { get; set; }
        public string OfferUD { get; set; }
        public string OrderTypeNM { get; set; }
        public string Currency { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string FactoryUD { get; set; }
        public string ShippedByFactoryUD { get; set; }
        public string LDS { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string PackagingMethodNM { get; set; }
        public string SpecialRequirement { get; set; }
        public int? CataloguePageNo { get; set; }
        public int? OrderQnt { get; set; }
        public int? TotalShippedQuantity { get; set; }
        public int? Qnt40HC { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? PurchasingPrice { get; set; }
        public decimal? PurchasingPriceIncludeDiff { get; set; }
        public decimal? TargetPrice { get; set; }
        public string RequestedDate { get; set; }
        public decimal? TransportationCostPerContainerInEUR { get; set; }
        public decimal? BonusPercent { get; set; }
        public decimal? CommissionPercent { get; set; }
        public decimal? DamagePercent { get; set; }
        public decimal? InterestPercent { get; set; }
        public decimal? LCCostPercent { get; set; }
        public string Remark { get; set; }
        public string PriceDifferenceCode { get; set; }
        public decimal? PriceDifferenceRate { get; set; }
        public string SoonestShippedDate { get; set; }
        public decimal? SaleAmountUSD { get; set; }
        public decimal? SaleAmountEUR { get; set; }
        public decimal? SaleAmountConvertedUSD { get; set; }
        public decimal? PurchasingAmountUSD { get; set; }
        public decimal? TargetPurchasingAmountUSD { get; set; }
        public decimal? TransportationUSD { get; set; }
        public decimal? DamageUSD { get; set; }
        public decimal? TargetDamageUSD { get; set; }
        public decimal? CommissionUSD { get; set; }
        public decimal? BonusUSD { get; set; }
        public decimal? InterestUSD { get; set; }
        public decimal? LCCostUSD { get; set; }
        public decimal? Delta6Amount { get; set; }
        public decimal? TargetDelta6Amount { get; set; }
        public int? StatusID { get; set; }
        public string StatusUpdatorName { get; set; }
        public int? StatusUpdatedBy { get; set; }
        public string StatusUpdatedDate { get; set; }
        public int? OrderTypeID { get; set; }
        public int? ShippedByFactoryID { get; set; }
        public int? FactoryID { get; set; }
        public int? ModelID { get; set; }
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
        public int? PackagingMethodID { get; set; }
        public int? ClientSpecialPackagingMethodID { get; set; }
        public int? ClientID { get; set; }
        public int? OfferSeasonID { get; set; }
        public int? OfferSeasonDetailID { get; set; }
        public int? OfferID { get; set; }
        public int? OfferLineID { get; set; }
        public int? OfferLineSparePartID { get; set; }
        public int? OfferLineSampleProductID { get; set; }
        public int? SaleOrderID { get; set; }
        public int? SaleOrderDetailID { get; set; }
        public int? SaleOrderDetailSparepartID { get; set; }
        public int? SaleOrderDetailSampleID { get; set; }
        public int? FactoryOrderID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryOrderSparepartDetailID { get; set; }
        public int? FactoryOrderSampleDetailID { get; set; }
        public int? ModelDefaultOptionID { get; set; }
        public int? QuotationOfferDirectionID { get; set; }
        public int PriceCacheID { get; set; }
        public int? PricingTeamMemberID { get; set; }
        public string PricingTeamMemberNM { get; set; }
        public int? SumOrderQnt1 { get; set; }
        public int? SumOrderQnt2 { get; set; }
        public int? SumOrderQnt3 { get; set; }
        public decimal? OrderQnt40HC { get; set; }
        public decimal? FactoryPrice1 { get; set; }
        public decimal? FactoryPrice2 { get; set; }
        public decimal? FactoryPrice3 { get; set; }
        public decimal? FurnindoPrice1 { get; set; }
        public decimal? FurnindoPrice2 { get; set; }
        public decimal? FurnindoPrice3 { get; set; }

        public decimal? NewTargetPrice { get; set; }
        public string NewTargetComment { get; set; }
        public bool IsAdditionalDataLoaded { get; set; }
        public bool IsSelected { get; set; }
        public bool UnConfirmable { get; set; }
    }
}
