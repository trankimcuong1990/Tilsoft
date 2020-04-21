using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.LoadingPlanMng
{
    public class OverviewData
    {
        public LoadingPlanOverView Data { get; set; }
        public List<DTO.LoadingPlanMng.User2> Users { get; set; }
    }
}
