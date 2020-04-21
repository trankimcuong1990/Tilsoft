using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOM.DTO
{
    public class ProductionItemUnitDTO
    {
        public int? ProductionItemID { get; set; }
        public int? UnitID { get; set; }
        public decimal? ConversionFactor { get; set; }
        public string UnitNM { get; set; }
    }
}
