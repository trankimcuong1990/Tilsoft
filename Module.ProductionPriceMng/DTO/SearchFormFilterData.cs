using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionPriceMng.DTO
{
    public class SearchFormFilterData
    {
        public List<Module.Support.DTO.ProductionItemType> ProductionItemTypes { get; set; }
        public List<int> Months { get; set; }
        public List<int> Years { get; set; }
    }
}
