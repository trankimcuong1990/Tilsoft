using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class FactoryRawMaterialGalleryDTO
    {
        public int FactoryRawMaterialGalleryID { get; set; }
        public string FactoryRawMaterialGalleryUD { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryGalleryFile { get; set; }
        public string FactoryGalleryThumbnail { get; set; }
        public bool FactoryGalleryHasChange { get; set; }
        public string FactoryGalleryNewFile { get; set; }
    }
}
