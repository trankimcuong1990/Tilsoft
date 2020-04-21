using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductOverviewRpt.DTO.PriceComparison
{
    public class ProductOptionDetailDTO
    {
        public int FactoryOrderDetailID { get; set; }
        public string Season { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? OrderQnt { get; set; }
        public string FactoryUD { get; set; }
        public string QuotationStatusNM { get; set; }
        public string PriceDifferenceCode { get; set; }
        public decimal? PurchasingPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? TargetPrice { get; set; }
        public string MaterialNM { get; set; }
        public string MaterialTypeNM { get; set; }
        public string MaterialColorNM { get; set; }
        public string FrameMaterialNM { get; set; }
        public string FrameMaterialColorNM { get; set; }
        public string SubMaterialNM { get; set; }
        public string SubMaterialColorNM { get; set; }
        public string BackCushionNM { get; set; }
        public string SeatCushionNM { get; set; }
        public string CushionColorNM { get; set; }
        public string CushionNM { get; set; }
        public string FSCTypeNM { get; set; }
        public string FSCPercentNM { get; set; }
        public string PackagingMethodNM { get; set; }
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
        public int? CushionID { get; set; }
        public int? FSCTypeID { get; set; }
        public int? FSCPercentID { get; set; }
        public bool? UseFSCLabel { get; set; }
        public int? PackagingMethodID { get; set; }
        public int? StatusID { get; set; }

        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string ImageSource { get; set; }
        public string ClientSpecialPackagingMethodNM { get; set; }
        public int? ClientSpecialPackagingMethodID { get; set; }
    }
}
