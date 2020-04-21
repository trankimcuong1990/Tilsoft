using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryQuotationMng.DTO
{
    public class SearchFilterData
    {
        public List<Support.DTO.Season> Seasons { get; set; }
        public List<Support.DTO.Factory> Factories { get; set; }
        public List<Support.DTO.QuotationStatus> QuotationStatuses { get; set; }
    }
}
