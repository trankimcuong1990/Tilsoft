using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class WarehouseItem
    {
        public string KeyID { get; set; }

        public int? GoodsID { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public string ProductType { get; set; }

        public string SelectedThumbnailImage { get; set; }

        public string SelectedFullImage { get; set; }

        public int? ModelID { get; set; }

        public int? MaterialID { get; set; }

        public int? MaterialTypeID { get; set; }

        public int? MaterialColorID { get; set; }

        public int? FrameMaterialID { get; set; }

        public int? FrameMaterialColorID { get; set; }

        public int? SubMaterialID { get; set; }

        public int? SubMaterialColorID { get; set; }

        public int? SeatCushionID { get; set; }

        public int? BackCushionID { get; set; }

        public int? CushionColorID { get; set; }

        public int? FSCTypeID { get; set; }

        public int? FSCPercentID { get; set; }

        public int? ModelPartID { get; set; }

        public int? PartID { get; set; }

        public string ModelUD { get; set; }

        public string ModelNM { get; set; }

        public int? ManufacturerCountryID { get; set; }

        public int? PackagingMethodID { get; set; }
    }
}
