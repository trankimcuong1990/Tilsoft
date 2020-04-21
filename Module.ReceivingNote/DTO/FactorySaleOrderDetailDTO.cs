using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceivingNote.DTO
{
    public class FactorySaleOrderDetailDTO
    {
        public FactorySaleOrderDetailDTO()
        {
            ProductionItemUnits = new List<ProductionItemUnit>();
        }
        public int FactorySaleOrderDetailID { get; set; }
        public Nullable<int> FactorySaleOrderID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> FactoryWarehouseID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string UnitNM { get; set; }
        public string FactoryWarehouseUD { get; set; }
        public string FactoryWarehouseNM { get; set; }

        public List<DTO.ProductionItemUnit> ProductionItemUnits { get; set; }
    }
}
