using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ImageGalleryMng
{
    public class ImageGallery
    {
        public int ImageGalleryID { get; set; }

        public int? ModelID { get; set; }

        public string FileUD { get; set; }

        [Required]
        public int? GalleryItemTypeID { get; set; }

        public int SeasonTypeID { get; set; }

        public int? MaterialID { get; set; }

        public int? MaterialTypeID { get; set; }

        public int? MaterialColorID { get; set; }

        public int? BackCushionID { get; set; }

        public int? SeatCushionID { get; set; }

        public int? CushionColorID { get; set; }

        public bool? IsNewProduct { get; set; }
        public bool IsFinalized { get; set; }

        public string Description { get; set; }

        public int? UpdatedBy { get; set; }
        public string ConcurrencyFlag_String { get; set; }

        public string UpdatedDate { get; set; }

        public string UpdatorName { get; set; }

        public string ModelNM { get; set; }

        public string ThumbnailLocation { get; set; }

        public string FileLocation { get; set; }
        public string SampleImportDate { get; set; }
        public string SampleImportBy { get; set; }

        public List<ImageGalleryClient> ImageGalleryClients { get; set; }
        public List<ImageGalleryVersion> ImageGalleryVersions { get; set; }

        public bool FileHasChange { get; set; }
        public string NewFile { get; set; }

        public int CurrentVersion { get; set; }
        public string TempDescription { get; set; }
    }
}