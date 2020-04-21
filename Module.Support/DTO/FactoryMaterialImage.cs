using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.Support.DTO
{
    public class FactoryMaterialImage
    {
        public int? FactoryMaterialImageID { get; set; }
        public int? FactoryMaterialID { get; set; }
        public string ImageFile { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }

        //support image field
        public bool? ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
    }
}