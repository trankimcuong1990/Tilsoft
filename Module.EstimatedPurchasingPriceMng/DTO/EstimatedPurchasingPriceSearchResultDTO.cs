using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EstimatedPurchasingPriceMng.DTO
{
    public class EstimatedPurchasingPriceSearchResultDTO
    {
        public int EstimatedPurchasingPriceID { get; set; }
        public string Season { get; set; }
        public decimal? EstimatedPrice { get; set; }
        public string Remark { get; set; }
        public string FactoryUD { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string PackagingMethodNM { get; set; }
        public string ClientSpecialPackagingMethodNM { get; set; }
        public string SpecialPackagingMethodDescription { get; set; }
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
        public decimal? NewEstimatedPrice { get; set; }
        public string NewRemark { get; set; }
        public List<DTO.HistoryDTO> HistoryDTOs { get; set; }
        public bool IsHistoryLoaded { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
    }
}
