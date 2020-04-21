using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DTO
{
    public class SupportData
    {
        public List<Module.Support.DTO.SamplePurpose> SamplePurposes { get; set; }
        public List<Module.Support.DTO.SampleTransportType> SampleTransportTypes { get; set; }
        public List<Module.Support.DTO.SampleOrderStatus> SampleOrderStatuses { get; set; }
        public List<Module.Support.DTO.Factory> Factories { get; set; }
    }
}
