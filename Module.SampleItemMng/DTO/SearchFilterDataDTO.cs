using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SampleItemMng.DTO
{
    public class SearchFilterDataDTO
    {
        public List<SampleProductStatusDTO> SampleProductStatusDTOs { get; set; }
        public List<DTO.SeasonDTO> SeasonDTOs { get; set; }      
    }
}
