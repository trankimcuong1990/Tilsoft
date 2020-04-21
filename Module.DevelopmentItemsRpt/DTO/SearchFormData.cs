using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevelopmentItemsRpt.DTO
{
    public class SearchFormData
    {
        public List<DTO.DevelopmentItemSearchResult> Data { get; set; }
        public List<Support.DTO.Season> seasons { get; set; }
        public List<Support.DTO.SampleProductStatus> sampleProductStatuses { get; set; }
    }
}
