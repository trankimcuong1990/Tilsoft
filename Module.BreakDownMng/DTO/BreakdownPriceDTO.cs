using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class BreakdownPriceDTO
    {
        public int ProductionItemID { get; set; }
        public int UnitID { get; set; }
        public string ItemNameVN { get; set; }
        public string ItemNameEN { get; set; }
        public string UnitNM { get; set; }
        public decimal AVFPrice { get; set; }
        public decimal AVTPrice { get; set; }
    }
}
