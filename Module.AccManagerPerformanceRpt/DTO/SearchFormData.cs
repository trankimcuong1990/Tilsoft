using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccManagerPerformanceRpt.DTO
{
    public class SearchFormData
    {
        public List<DTO.AccManagerPerformanceDTO> AccManagerPerformanceDTOs { get; set; }
        public List<DTO.AccManagerPerformanceDeltaDTO> AccManagerPerformanceDeltaDTOs { get; set; }
    }
}
