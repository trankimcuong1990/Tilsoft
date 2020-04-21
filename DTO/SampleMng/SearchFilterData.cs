using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SampleMng
{
    public class SearchFilterData
    {
        public List<Support.Season> Seasons { get; set; }
        public List<Support.SamplePurpose> SamplePurposes { get; set; }
        public List<Support.SampleTransportType> SampleTransportTypes { get; set; }
        public List<Support.SampleOrderStatus> SampleOrderStatuses { get; set; }
    }
}
