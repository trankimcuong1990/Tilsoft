using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionSummaryRpt.DTO
{
    public class SearchFormDTO
    {
        public List<ProductionSummaryDTO> ProductionSummaries { get; set; }
        public List<ProductionSummaryDetailDTO> ProductionSummaryDetails { get; set; }
        public List<WorkCenterDTO> WorkCenters { get; set; }

        public SearchFormDTO()
        {
            ProductionSummaries = new List<ProductionSummaryDTO>();
            ProductionSummaryDetails = new List<ProductionSummaryDetailDTO>();
            WorkCenters = new List<WorkCenterDTO>();
        }
    }
}
