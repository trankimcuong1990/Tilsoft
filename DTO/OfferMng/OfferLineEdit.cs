using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.OfferMng
{
    public class OfferLineEdit
    {
        public int OfferLineID { get; set; }

        public int ModelID { get; set; }

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
    }
}