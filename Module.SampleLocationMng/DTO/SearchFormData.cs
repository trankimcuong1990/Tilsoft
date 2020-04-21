using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SampleLocationMng.DTO
{
    public class SearchFormData
    {
        public List<SampleProductLocationSearchResultData> SearchData { get; set; }
        public List<Support.DTO.Season> Seasons { get; set; }
        public List<SupportFactoryLocationData> Locations { get; set; }
        public List<Support.DTO.Client> Clients { get; set; }
        public List<Support.DTO.Factory> Factories { get; set; }
        public List<Support.DTO.SampleProductStatus> Statuses { get; set; }
    }
}
