using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseTransferMng.DTO
{
    public class StockQntFromWarehouse
    {
        public long PrimaryID { get; set; }
        public int? FactoryWarehouseID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? StockQnt { get; set; }
    }
}
