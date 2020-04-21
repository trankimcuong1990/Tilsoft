using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt.DTO
{
    public class CompanyDetailData
    {
        public List<DTO.CompanyDetail> CompanyDetails { get; set; }
        public List<DTO.CompanyDetailWeeklyDetail> CompanyDetailWeeklyDetails { get; set; }
        public List<DTO.CompanyDetailWeeklyOverview> CompanyDetailWeeklyOverviews { get; set; }
    }
}
