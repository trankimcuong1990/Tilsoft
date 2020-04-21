using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryBreakdownMng.DTO
{
    public class SearchFormData
    {
        public List<DTO.FactoryBreakdownSearchResultDTO> Data { get; set; }
        public List<DTO.FactoryBreakdownStatistic> Statistic { get; set; }
    }
}
