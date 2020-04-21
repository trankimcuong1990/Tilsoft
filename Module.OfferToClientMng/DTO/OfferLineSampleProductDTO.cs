using System;

namespace Module.OfferToClientMng.DTO
{
    public class OfferLineSampleProductDTO
    {
        public int OfferLineSampleProductID { get; set; }
        public Nullable<int> OfferID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> OfferSeasonDetailQuantity { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public string Remark { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> OrderQntIn40HC { get; set; }
        public Nullable<decimal> PlaningPurchasingPrice { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public int OfferSeasonDetailID { get; set; }
        public Nullable<int> PlaningPurchasingPriceSourceID { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public string PackagingMethodText { get; set; }
        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientOrderNumber { get; set; }
        public string ClientEANCode { get; set; }
    }
}
