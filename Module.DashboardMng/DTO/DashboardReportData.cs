using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardReportData
    {
        // User
        public List<DTO.UserDataRpt> UserDataRpt { get; set; }
        public List<DTO.HitCountDataRpt> HitCountDataRpt { get; set; }
    }
}   
