using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
    public class SearchFilterData
    {
        public List<Support.DTO.Season> Seasons { get; set; }
        public List<Support.DTO.SamplePurpose> SamplePurposes { get; set; }
        public List<Support.DTO.SampleTransportType> SampleTransportTypes { get; set; }
        public List<Support.DTO.SampleOrderStatus> SampleOrderStatuses { get; set; }
        public List<Support.DTO.Factory> Factorys { get; set; }
        public List<DTO.AccountManagerDTO> AccountManagerDTOs { get; set; }     
    }
}
