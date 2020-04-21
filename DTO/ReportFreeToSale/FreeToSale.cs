using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ReportFreeToSale
{
    public class FreeToSale
    {
        public string KeyID { get; set; }

        public int? GoodsID { get; set; }

        public string ProductType { get; set; }

        public int? ProductStatusID { get; set; }

        public string ArticleCode { get; set; }

        public string SelectedThumbnailImage { get; set; }

        public string SelectedFullImage { get; set; }

        public string Description { get; set; }

        public string MaterialNM { get; set; }

        public string MaterialTypeNM { get; set; }

        public string MaterialColorNM { get; set; }

        public string CushionNM { get; set; }

        public string OverallDimL { get; set; }

        public string OverallDimW { get; set; }

        public string OverallDimH { get; set; }

        public int? FTSQnt { get; set; }

        public decimal? FTSQntIn40HC { get; set; }

        public decimal? SalePrice { get; set; }

        public bool? IsActiveFreeToSale { get; set; }

        public bool? IsMatchedImage { get; set; }

        public string FreeToSaleCategoryNM { get; set; }

        public string CushionImage { get; set; }

        public string MaterialImage { get; set; }

        public int? Qnt20DC { get; set; }

        public int? Qnt40DC { get; set; }

        public int? Qnt40HC { get; set; }

        public int? DisplayOrder { get; set; }

        public string ProductThumbnailImage { get; set; }

        public string ProductGardenThumbnailImage { get; set; }

        public string ModelThumbnailImage { get; set; }

        public string ProductFullImage { get; set; }

        public string ProductGardenFullImage { get; set; }

        public string ModelFullImage { get; set; }



    }
}