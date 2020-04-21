using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakdownPriceListMng.DTO
{
    public class BreakdownPriceListSearchDTO
    {
        public Nullable<int> ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemVNNM { get; set; }
        public string UnitNM { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public Nullable<decimal> AVFPrice { get; set; }
        public Nullable<decimal> AVTPrice { get; set; }
    }
}
