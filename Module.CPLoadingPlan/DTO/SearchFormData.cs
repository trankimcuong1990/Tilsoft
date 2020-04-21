using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CPLoadingPlan.DTO
{
    public class SearchFormData
    {
        public List<DTO.LoadingPlanSearchResultDTO> Data { get; set; }
        public List<DTO.OrderInfoDTO> OrderInfoDTOs { get; set; }
    }
}
