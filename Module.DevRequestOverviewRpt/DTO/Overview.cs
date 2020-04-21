using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevRequestOverviewRpt.DTO
{
    public class Overview
    {
        public int? UserID { get; set; }
        public string EmployeeNM { get; set; }
        public int? TotalRequest { get; set; }
        public int? TotalResolvedRequest { get; set; }
        public int? TotalPendingRequest { get; set; }
        public int? NotYetEstRequest { get; set; }
        public int? TotalEstHour { get; set; }
        public int? TotalActHour { get; set; }
    }
}
