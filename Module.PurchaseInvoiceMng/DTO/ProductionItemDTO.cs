using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseInvoiceMng.DTO
{
    public class ProductionItemDTO
    {
        public int ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public string FactoryWarehouseNM { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public int? FactoryWarehouseID { get; set; }
    }
}
