using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryOrderMng
{
    public class FactoryOrderSampleDetail
    {
        public int SaleOrderDetailSampleID { get; set; }
        public int? SaleOrderID { get; set; }
        public int? OfferLineSampleProductID { get; set; }
        public int? OrderQnt { get; set; }
        public decimal? SalePrice { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public int? RowIndex { get; set; }
        public int? OfferSeasonTypeID { get; set; }
        public int? ClientID { get; set; }
        public decimal? OrderQntIn40HC { get; set; }
        public decimal? PlaningPurchasingPrice { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public int? PlaningPurchasingPriceSourceID { get; set; }
        public int? Qnt40HC { get; set; }
        public string PackagingMethodText { get; set; }

        public int FactoryOrderSampleDetailID { get; set; }
        public int? FactoryOrderID { get; set; }
        public int? TotalQnt { get; set; }
        public int? SaleOrderQntSample { get; set; }
    }
}
