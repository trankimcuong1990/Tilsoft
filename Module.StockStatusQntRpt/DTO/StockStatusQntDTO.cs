using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StockStatusQntRpt.DTO
{
    public class StockStatusQntDTO
    {
        public long PrimaryID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public decimal? StockQnt { get; set; }
        public int? Minimum { get; set; }
        public decimal? Average { get; set; }
        public int? Maximum { get; set; }
        public int? FactoryWarehouseID { get; set; }
        public int? ProductionItemGroupID { get; set; }
        public string ProductionItemGroupNM { get; set; }
        public string FactoryWarehouseNM { get; set; }
        int? CompanyID { get; set; }
    }
}
