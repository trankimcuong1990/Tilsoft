using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseTransferMng.DTO
{
    public class ProductionItemDTO
    {
        public int ProductionItemID { get; set; }

        public string ProductionItemUD { get; set; }

        public string ProductionItemNM { get; set; }

        public int? UnitID { get; set; }

        public int? FromFactoryWarehouseID { get; set; }

        public int? ToFactoryWarehouseID { get; set; }

        public int? PrimaryID { get; set; }

        public decimal? StockQnt { get; set; }

        public List<ProductionItemUnitDTO> ProductionItemUnits { get; set; }

        public ProductionItemDTO()
        {
            ProductionItemUnits = new List<ProductionItemUnitDTO>();
        }
    }
}
