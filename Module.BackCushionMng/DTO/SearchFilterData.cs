using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BackCushionMng.DTO
{
    public class SearchFilterData
    {
        public List<Support.DTO.Season> Seasons { get; set; }
        public List<Support.DTO.YesNo> YesNoValues { get; set; }
        public List<Support.DTO.ProductGroup> ProductGroups { get; set; }
    }
}
