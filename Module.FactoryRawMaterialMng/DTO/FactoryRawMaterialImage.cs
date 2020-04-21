using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class FactoryRawMaterialImage
    {
        public int FactoryRawMaterialImageID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public bool HasChange { get; set; }
        public string NewFile { get; set; }
    }
}
