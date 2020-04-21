using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.OfferMng
{
   public class OfferlineOverView
    {
        public int? OfferLineID { get; set; }

        public int? OfferID { get; set; }

        public int? RowIndex { get; set; }

        public int? ModelID { get; set; }

        public int? MaterialID { get; set; }

        public int? MaterialTypeID { get; set; }

        public int? MaterialColorID { get; set; }

        public int? FrameMaterialID { get; set; }

        public int? FrameMaterialColorID { get; set; }

        public int? CushionID { get; set; }

        public int? CushionColorID { get; set; }

        public int? SubMaterialID { get; set; }

        public int? SubMaterialColorID { get; set; }

        public int? PackagingMethodID { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? ManufacturerCountryID { get; set; }

        public int? POLID { get; set; }

        public decimal? IncreasePercent { get; set; }

        public decimal? TransportationFee { get; set; }

        public decimal? CommissionPercent { get; set; }

        public decimal? FinalPrice { get; set; }

        public string Remark { get; set; }

        public bool? IsClientSelected { get; set; }

        public int? ClientQuantity { get; set; }

        public string CushionRemark { get; set; }

        public string ClientArticleCode { get; set; }

        public string ClientArticleName { get; set; }

        public string ClientOrderNumber { get; set; }

        public string ClientEANCode { get; set; }

        public decimal? OrderQntIn40HC { get; set; }

        public decimal? CommissionAmount { get; set; }

        public decimal? SurchargeAmount { get; set; }

        public decimal? SubTotalAmount { get; set; }

        public decimal? VATAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        public string ModelUD { get; set; }

        public string ProductTypeNM { get; set; }

        public int? Qnt40HC { get; set; }

        public decimal? GrossWeight { get; set; }

        public string ProductFileLocation { get; set; }

        public string ProductThumbnailLocation { get; set; }

        public int? BackCushionID { get; set; }
        public int? SeatCushionID { get; set; }

        public int? FSCTypeID { get; set; }

        public int? FSCPercentID { get; set; }

        public bool UseFSCLabel { get; set; }

        public string PODName { get; set; }
        public string ModelStatusNM { get; set; }


        //SUPPORT PRODUC INFO
        public bool IsEditing { get; set; }

        public string ModelNM { get; set; }

        public string MaterialText { get; set; }

        public string FrameText { get; set; }

        public string SubMaterialText { get; set; }

        public string CushionText { get; set; }

        public string FSCText { get; set; }

        public string MaterialThumbnailLocation { get; set; }

        public string FrameMaterialThumbnailLocation { get; set; }

        public string SubMaterialColorThumbnailLocation { get; set; }

        //public string CushionThumbnailLocation { get; set; }

        public string CushionColorThumbnailLocation { get; set; }

        public string BackCushionThumbnailLocation { get; set; }

        public string SeatCushionThumbnailLocation { get; set; }

        public bool? IsCreatedOrder { get; set; }

        public List<OfferLinePriceOptionOverView> OfferLinePriceOptions { get; set; }

        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? SalePriceCalculated { get; set; }
        public decimal AVTPurchasingPrice { get; set; }
        public int AVTPurchasingPriceStatusID { get; set; }

    }
}
