using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SaleOrderMng
{
    public class SaleOrderDetailSample
    {
        public int SaleOrderDetailSampleID { get; set; }
        public int? OfferID { get; set; }
        public int? Quantity { get; set; }
        public int? RowIndex { get; set; }
        public int? OfferSeasonTypeID { get; set; }
        public bool? IsFactoryOrderCreated { get; set; }
        public int? ClientID { get; set; }
        public int? SaleOrderID { get; set; }
        public int? OfferLineSampleProductID { get; set; }
        public decimal? SalePrice { get; set; }
        public string Remark { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public decimal? OrderQntIn40HC { get; set; }
        public decimal? PlaningPurchasingPrice { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public int? PlaningPurchasingPriceSourceID { get; set; }
        public int? Qnt40HC { get; set; }
        public string PackagingMethodText { get; set; }
        public bool IsSelected { get; set; }
        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientOrderNumber { get; set; }
        public string ClientEANCode { get; set; }
    }
}
