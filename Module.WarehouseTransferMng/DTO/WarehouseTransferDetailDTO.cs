using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseTransferMng.DTO
{
    public class WarehouseTransferDetailDTO
    {
        public int WarehouseTransferDetailID { get; set; }

        public int? WarehouseTransferID { get; set; }

        public int? ProductionItemID { get; set; }

        public int? UnitID { get; set; }

        public int? FromFactoryWarehouseID { get; set; }

        public decimal? ReceivedQnt { get; set; }

        public int? ToFactoryWarehouseID { get; set; }

        public decimal? DeliveriedQnt { get; set; }

        public string Remark { get; set; }

        public string ProductionItemUD { get; set; }

        public string ProductionItemNM { get; set; }

        public string UnitNM { get; set; }

        public string FromFactoryWarehouseUD { get; set; }

        public string FromFactoryWarehouseNM { get; set; }

        public string ToFactoryWarehouseUD { get; set; }

        public string ToFactoryWarehouseNM { get; set; }

        public decimal? StockQnt { get; set; }

        public List<ProductionItemUnitDTO> ProductionItemUnits { get; set; }

        public WarehouseTransferDetailDTO()
        {
            ProductionItemUnits = new List<ProductionItemUnitDTO>();
        }
    }
}
