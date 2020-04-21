using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ImageGalleryMng
{
    public class ImageGalleryClient
    {
        public int ImageGalleryClientID { get; set; }

        public int? ClientID { get; set; }

        public string ClientUD { get; set; }

    }
}