using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.OfferMng
{
    public class Model
    {
        public int? ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int? PackagingMethodID { get; set; }
        public int? ManufacturerCountryID { get; set; }
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
        public string ArticleCode { get; set; }
        public string Description { get; set; }
    }
}
