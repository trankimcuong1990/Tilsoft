using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryWarehouse.DTO
{
    public class PalletOverview
    {
        public string FactoryWarehousePalletUD { get; set; }
        public string FactoryWarehousePalletNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public decimal TotalBalance { get; set; }
        public string Unit { get; set; }
        public int FactoryWarehousePalletID { get; set; }
        public int ProductionItemID { get; set; }
        public int FactoryWarehouseID { get; set; }
    }
}
