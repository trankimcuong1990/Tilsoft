using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOM.DTO
{
    public class ProductionItemDTO
    {
        public int? ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public string Unit { get; set; }
        public int? UnitID { get; set; }
        public string UnitNM { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string UpdatorName { get; set; }

        //support save image
        public bool? ProductImage_HasChange { get; set; }
        public string ProductImage_NewFile { get; set; }
        public decimal? WastagePercent { get; set; }

        //unit
        public List<ProductionItemUnitDTO> ProductionItemUnitDTOs { get; set; }
    }
}
