using System;

namespace Module.OfferToClientMng.DTO
{
    public class OfferSeasonDetailDTO
    {
        public long KeyID { get; set; }
        public int OfferSeasonID { get; set; }
        public Nullable<int> OfferSeasonTypeID { get; set; }
        public string Season { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> CommissionPercent { get; set; }
        public Nullable<decimal> TransportationFee { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public string Remark { get; set; }
        public Nullable<int> OfferSeasonDetailID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public Nullable<decimal> PlaningPurchasingPrice { get; set; }
        public string ApprovedDate { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> PartID { get; set; }
        public Nullable<int> PlaningPurchasingPriceSourceID { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public string PackagingMethodText { get; set; }        
        public int OfferLineType { get; set; }
        public string ProductFileLocation { get; set; }
        public string ProductThumbnailLocation { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public string OfferSeasonTypeNM { get; set; }
        public Nullable<int> SaleID { get; set; }      
    }
}
