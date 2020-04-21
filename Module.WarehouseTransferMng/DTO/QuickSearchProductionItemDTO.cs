using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseTransferMng.DTO
{
    public class QuickSearchProductionItemDTO
    {
        public List<ProductionItemDTO> ProductionItems { get; set; }

        public List<ProductionItemUnitDTO> ProductionItemUnits { get; set; }

        public QuickSearchProductionItemDTO()
        {
            ProductionItems = new List<ProductionItemDTO>();
            ProductionItemUnits = new List<ProductionItemUnitDTO>();
        }
    }
}
