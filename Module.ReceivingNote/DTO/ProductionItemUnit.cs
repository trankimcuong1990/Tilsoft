using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceivingNote.DTO
{
    public class ProductionItemUnit
    {
        public int? ProductionItemID { get; set; }
        public int? UnitID { get; set; }
        public string UnitNM { get; set; }
        public decimal? ConversionFactor { get; set; }
        public int? SubUnitID { get; set; }


    }
}
