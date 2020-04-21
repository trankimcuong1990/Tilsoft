using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class ProductionItemDTO
    {
        public int ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemNMEN { get; set; }
        public int UnitID { get; set; }
        public string UnitNM { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public int ProductionItemTypeID { get; set; }
        public decimal? ItemCost { get; set; }
    }
}
