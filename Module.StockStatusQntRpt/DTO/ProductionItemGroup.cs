using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StockStatusQntRpt.DTO
{
    public class ProductionItemGroup
    {
        public int ProductionItemGroupID { get; set; }
        public string ProductionItemGroupUD { get; set; }
        public string ProductionItemGroupNM { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public decimal? WastagePercent { get; set; }
        public string UpdatorNM { get; set; }
    }
}
