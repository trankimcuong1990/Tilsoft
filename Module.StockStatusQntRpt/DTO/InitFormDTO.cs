using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StockStatusQntRpt.DTO
{
    public class InitFormDTO
    {
        public List<DTO.ProductionItemGroup> ProductionItemGroups { get; set; }
        public List<DTO.FactoryWarehouseDTO> FactoryWarehouseDtos { get; set; }

        public InitFormDTO()
        {
            ProductionItemGroups = new List<DTO.ProductionItemGroup>();
            FactoryWarehouseDtos = new List<DTO.FactoryWarehouseDTO>();
        }
    }
}
