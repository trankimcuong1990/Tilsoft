using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt.DTO
{
    public class ReportFormData
    {
        public List<ActiveBrowser> ActiveBrowsers { get; set; }
        public List<ActiveCompany> ActiveCompanies { get; set; }
        public List<ActiveCompanyWeeklyHit> ActiveCompanyWeeklyHits { get; set; }
        public List<ActiveIP> ActiveIPs { get; set; }
        public List<ActiveModule> ActiveModules { get; set; }
        public List<ActiveUser> ActiveUsers { get; set; }

        public List<HitCount> HitCounts { get; set; }
        public List<UserMutation> UserMutations { get; set; }
    }
}
