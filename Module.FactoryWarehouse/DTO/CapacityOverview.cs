using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryWarehouse.DTO
{
    public class CapacityOverview
    {
        public long PrimaryID { get; set; }
        public int? FactoryWarehousePalletID { get; set; }
        public string FactoryWarehousePalletUD { get; set; }
        public string FactoryWarehousePalletNM { get; set; }
        public int? Capacity { get; set; }
        public decimal? TotalBalance { get; set; }
        public int? FactoryWarehouseID { get; set; }
    }
}
