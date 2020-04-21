using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryLoadingPlanMng.DTO
{
    public class SearchFilterData
    {
        public List<Module.Support.DTO.Season> Seasons { get; set; }
        public List<Module.Support.DTO.Factory> Factories { get; set; }
    }
}
