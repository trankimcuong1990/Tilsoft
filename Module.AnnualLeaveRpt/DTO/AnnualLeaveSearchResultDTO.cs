using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AnnualLeaveRpt.DTO
{
    public class AnnualLeaveSearchResultDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeNM { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string CompanyNM { get; set; }     
        public Nullable<decimal> RequestedTimeOff { get; set; }
        public int? AffectedYear { get; set; }
        public Nullable<decimal> TotalAvailableDay { get; set; }
        public Nullable<decimal> TotalDaysOff { get; set; }
        public Nullable<decimal> TotalOtherLeave { get; set; }
        public bool HasLeft { get; set; }
    }
}
