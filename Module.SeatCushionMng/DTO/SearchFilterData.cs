using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SeatCushionMng.DTO
{
    public class SearchFilterData
    {
        public List<Season> Seasons { get; set; }
        public List<YesNo> YesNoValues { get; set; }
        public List<spProductGroup> ProductGroups { get; set; }
    }
}
