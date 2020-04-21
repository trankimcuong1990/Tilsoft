using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SummarySalesRpt.DTO
{
    public class SearchForm
    {
        public List<DTO.CustomerData> CustomerDatas { get; set; }
        public List<DTO.ProductionItemData> ProductionItemDatas { get; set; }
    }
}
