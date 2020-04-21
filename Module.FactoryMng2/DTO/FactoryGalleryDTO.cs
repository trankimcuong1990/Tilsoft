using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2.DTO
{
    public class FactoryGalleryDTO
    {
        public int FactoryGalleryID { get; set; }
        public string FactoryGalleryUD { get; set; }
        public int? FactoryID { get; set; }
        public string FactoryGalleryFile { get; set; }
        public string FactoryGalleryThumbnail { get; set; }
        public bool FactoryGalleryHasChange { get; set; }
        public string FactoryGalleryNewFile { get; set; }
    }
}
