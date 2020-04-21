using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseConnectMng.DTO
{
    public class ProductMediaDTO
    {
        public int ModelID { get; set; }
        public string GalleryItemTypeNM { get; set; }
        public string FriendlyName { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
    }
}
