using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SampleOrderOverviewRpt.DTO
{
    public class InitFormData
    {
        public List<Support.DTO.Season> Seasons { get; set; }
        public List<Support.DTO.Factory> Factories { get; set; }
        public List<Support.DTO.SampleProductStatus> SampleProductStatuses { get; set; }
        public List<Support.DTO.SampleOrderStatus> SampleOrderStatuses { get; set; }
    }
}
