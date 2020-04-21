using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ReportStockList
{
    public class StockList
    {
        public string KeyID { get; set; }

        public int? GoodsID { get; set; }

        public string ProductType { get; set; }

        public int? ProductStatusID { get; set; }
        
        public string ProductStatusNM { get; set; }

        public string SelectedThumbnailImage { get; set; }

        public string SelectedFullImage { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public int? Qnt40HC { get; set; }

        public int? PhysicalQnt { get; set; }

        public decimal? PhysicalQntIn40HC { get; set; }

        public int? OnSeaQnt { get; set; }

        public int? TobeShippedQnt { get; set; }

        public int? ReservationQnt { get; set; }

        public int? FTSQnt { get; set; }

        public bool? IsActiveFreeToSale { get; set; }

        public int? FreeToSaleCategoryID { get; set; }

        public bool? IsMatchedImage { get; set; }

        public string FreeToSaleCategoryNM { get; set; }

        public string OverallDimL { get; set; }

        public string OverallDimW { get; set; }

        public string OverallDimH { get; set; }

        public string MaterialNM { get; set; }

        public string MaterialTypeNM { get; set; }

        public string MaterialColorNM { get; set; }

        public string FrameMaterialNM { get; set; }

        public string FrameMaterialColorNM { get; set; }

        public string SubMaterialNM { get; set; }

        public string SubMaterialColorNM { get; set; }

        public string SeatCushionNM { get; set; }

        public string BackCushionNM { get; set; }

        public string CushionColorNM { get; set; }

        public string CushionImage { get; set; }

        public string MaterialImage { get; set; }

        public string ProductThumbnailImage { get; set; }

        public string ProductGardenThumbnailImage { get; set; }

        public string ModelThumbnailImage { get; set; }

        public string ProductFullImage { get; set; }

        public string ProductGardenFullImage { get; set; }

        public string ModelFullImage { get; set; }

        public string WarehouseAreaUD { get; set; }

        public string QntPerWarehouseArea { get; set; }
        

    }
}