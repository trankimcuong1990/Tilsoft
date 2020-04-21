using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AdditionalConditionMng.DTO
{
    public class SearchFormData
    {
        public List<DTO.AdditionalConditionDTO> Data { get; set; }
        public List<DTO.AdditionalConditionSearch> AdditionalConditionSearch { get; set; }
    }
}
