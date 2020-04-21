using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItem.DTO
{
    public class SearchFilterData
    {
        public List<DTO.ProductionItemGroup> ProductionItemGroups { get; set; }
        public List<DTO.ProductionItemType> ProductionItemMaterialTypes { get; set; }
        public List<DTO.ProductionItemSpec> ProductionItemSpecs { get; set; }
        public List<DTO.FactoryWarehouse> FactoryWarehouses { get; set; }
        public List<DTO.ProductionItemTypeSupport> productionItemTypeSupports { get; set; }
    }
}
