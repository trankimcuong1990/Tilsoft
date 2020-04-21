using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MIDeltaByClientOSRpt.DTO
{
    public class SearchFormData
    {
        public List<DTO.DeltaByClientDTO> Data { get; set; }
        public List<DTO.AccountManagerSummaryDTO> AccountManagerSummaryDTOs { get; set; }
    }
}
