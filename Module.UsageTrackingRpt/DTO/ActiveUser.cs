using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt.DTO
{
    public class ActiveUser
    {
        public int UserID { get; set; }
        public string EmployeeNM { get; set; }
        public int TotalHit { get; set; }
        public string InternalCompanyNM { get; set; }
        public decimal AVGHit { get; set; }
    }
}
