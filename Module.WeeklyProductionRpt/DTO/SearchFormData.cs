using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WeeklyProductionRpt.DTO
{
    public class SearchFormData
    {
        public List<FrameOverview> Data { get; set; }
        public List<Module.Support.DTO.Season> Seasons { get; set; }
    }
}
