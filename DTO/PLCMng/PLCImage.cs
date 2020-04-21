using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.PLCMng
{
    public class PLCImage
    {
        public int PLCImageID { get; set; }
        public int ImageTypeID { get; set; }
        public string ImageFile { get; set; }
        public string PLCImageTypeNM { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string ImageFile_FullSizeUrl { get; set; }

        public bool ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
    }
}