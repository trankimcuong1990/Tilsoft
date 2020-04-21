using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPLCMng.DTO
{
    public class PLCImage
    {
        public int PLCImageID { get; set; }
        public int? ImageTypeID { get; set; }
        public string ImageFile { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }

        public bool ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
    }
}
