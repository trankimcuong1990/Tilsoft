using System;

namespace Module.AnnualLeaveMng.DTO
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeUD { get; set; }
        public string EmployeeNM { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<decimal> TotalAvailableTime { get; set; }
        public Nullable<decimal> TotalDaysOff { get; set; }
        public int ReportToID { get; set; }
    }
}
