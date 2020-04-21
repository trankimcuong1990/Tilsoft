using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryWarehouse.DTO
{
    public class FactoryWarehousePallet
    {
        public int? FactoryWarehousePalletID { get; set; }
        public int FactoryWarehouseID { get; set; }
        public string FactoryWarehousePalletUD { get; set; }
        public string FactoryWarehousePalletNM { get; set; }
        public int? CompanyID { get; set; }
        public int? Capacity { get; set; }
    }
}
