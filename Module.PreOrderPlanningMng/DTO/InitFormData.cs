using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PreOrderPlanningMng.DTO
{
    public class InitFormData
    {
        public List<Support.DTO.Season> SupportSeason { get; set; }

        public List<Support.DTO.Factory> SupportFactory { get; set; }
    }
}
