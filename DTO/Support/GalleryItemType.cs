using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class GalleryItemType
    {
        public int GalleryItemTypeID { get; set; }
        public string GalleryItemTypeNM { get; set; }
    }
}