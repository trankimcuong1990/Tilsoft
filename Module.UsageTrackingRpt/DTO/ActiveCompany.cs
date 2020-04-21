using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt.DTO
{
    public class ActiveCompany
    {
        public int CompanyID { get; set; }
        public string InternalCompanyNM { get; set; }
        public int AvgHit { get; set; }
        public int TotalHit { get; set; }

    }
}
