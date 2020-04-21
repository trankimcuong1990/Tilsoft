using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ImageGalleryMng
{
    public class ImageGallerySearchResult
    {
        public int ImageGalleryID { get; set; }

        public bool? IsNewProduct { get; set; }
        public bool IsFinalized { get; set; }

        public string GalleryItemTypeNM { get; set; }

        public string ModelNM { get; set; }

        public string SeasonTypeNM { get; set; }

        public string MaterialNM { get; set; }

        public string MaterialTypeNM { get; set; }

        public string MaterialColorNM { get; set; }

        public string BackCushionNM { get; set; }

        public string SeatCushionNM { get; set; }

        public string CushionColorNM { get; set; }

        public string TempDescription { get; set; }
        public string Description { get; set; }

        public string UpdatorName { get; set; }

        public string UpdatedDate { get; set; }

        public int? GalleryItemTypeID { get; set; }

        public string ThumbnailLocation { get; set; }

        public string FileLocation { get; set; }
        public string Client { get; set; }
        public string SampleImportDate { get; set; }
        public string SampleImportBy { get; set; }
        public int CurrentVersion { get; set; }

        public int UpdatedBy { get; set; }
    }
}