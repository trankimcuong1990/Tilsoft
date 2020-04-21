using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class OfferItemDefaultPropertiesDTO
    {
        public int ModelID { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public Nullable<int> MaterialTypeID { get; set; }
        public Nullable<int> MaterialColorID { get; set; }
        public Nullable<int> FrameMaterialID { get; set; }
        public Nullable<int> FrameMaterialColorID { get; set; }
        public Nullable<int> SubMaterialID { get; set; }
        public Nullable<int> SubMaterialColorID { get; set; }
        public Nullable<int> SeatCushionID { get; set; }
        public Nullable<int> BackCushionID { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public Nullable<int> FSCTypeID { get; set; }
        public Nullable<int> FSCPercentID { get; set; }
        public Nullable<int> ManufacturerCountryID { get; set; }
        public Nullable<int> PackagingMethodID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string MaterialText { get; set; }
        public string FrameText { get; set; }
        public string SubMaterialText { get; set; }
        public string CushionText { get; set; }
        public string FSCText { get; set; }
        public string MaterialThumbnailLocation { get; set; }
        public string FrameMaterialThumbnailLocation { get; set; }
        public string SubMaterialColorThumbnailLocation { get; set; }
        public string CushionColorThumbnailLocation { get; set; }
        public string BackCushionThumbnailLocation { get; set; }
        public string SeatCushionThumbnailLocation { get; set; }
        public string ProductFileLocation { get; set; }
        public string ProductThumbnailLocation { get; set; }

    }
}
