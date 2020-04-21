using System;

namespace Module.AnnualLeaveMng.DTO
{
    public class AnnualLeaveSearchResultDTO
    {
        public int AnnualLeaveTrackingDetailID { get; set; }
        public Nullable<int> AnnualLeaveTrackingID { get; set; }
        public Nullable<int> LeaveTypeID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public Nullable<decimal> NumberOfDays { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string StatusNM { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string EmployeeNM { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string CompanyNM { get; set; }
        public Nullable<int> UpdatedID { get; set; }
        public string UpdatedNM { get; set; }
        public Nullable<int> ApproveBy { get; set; }
    }
}
