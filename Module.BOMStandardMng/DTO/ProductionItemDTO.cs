using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOMStandardMng.DTO
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
        public List<ProductionItemUnitDTO> ProductionItemUnitDTOs { get; set; }
    }
}
