using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ImageGalleryMng
{
    public class ImageGalleryVersion
    {
        public int ImageGalleryVersionID { get; set; }
        public int? Version { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string UpdatorName { get; set; }
        public bool HasChange { get; set; }
        public string NewFile { get; set; }
    }
}