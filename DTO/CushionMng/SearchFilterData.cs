using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CushionMng
{
    public class SearchFilterData
    {
        public List<DTO.Support.Season> Seasons { get; set; }
        public List<DTO.Support.YesNo> YesNoValues { get; set; }
        public List<DTO.Support.CushionType> CushionTypes { get; set; }
    }
}
