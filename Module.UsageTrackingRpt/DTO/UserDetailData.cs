using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt.DTO
{
    public class UserDetailData
    {
        public List<DTO.UserDetail> UserDetails { get; set; }
        public List<DTO.UserDetailWeeklyDetail> UserDetailWeeklyDetails { get; set; }
        public List<DTO.UserDetailWeeklyOverview> UserDetailWeeklyOverviews { get; set; }
    }
}
