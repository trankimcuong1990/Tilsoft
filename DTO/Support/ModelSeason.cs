using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class ModelSeason
    {
        public int ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int? ProductTypeID { get; set; }
        public int? ManufacturerCountryID { get; set; }
        public string RangeName { get; set; }
        public string Collection { get; set; }
        public int? ModelStatusID { get; set; }
        public string Season { get; set; }
        public int? ProductGroupID { get; set; }
        public int? ProductCategoryID { get; set; }
        public string FileLocation { get; set; }
        public int? PackagingMethodID { get; set; }
        public int? Qnt40HC { get; set; }

        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }

        public string SeatDimL { get; set; }
        public string SeatDimW { get; set; }
        public string SeatDimH { get; set; }

        public string ArmDimL { get; set; }
        public string ArmDimW { get; set; }
        public string ArmDimH { get; set; }
        
        public string ThumbnailLocation { get; set; }
        public string ManufacturerCountryUD { get; set; }

        //default option
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
        public string ArticleCode { get; set; }
        public string Description { get; set; }

        public decimal? IndicatedSellPriceUSD { get; set; }
        public decimal? IndicatedSellPriceEUR { get; set; }
    }
}
