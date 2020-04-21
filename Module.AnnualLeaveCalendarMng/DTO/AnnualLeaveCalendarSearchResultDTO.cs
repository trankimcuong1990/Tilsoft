namespace Module.AnnualLeaveCalendarMng.DTO
{
    public class AnnualLeaveCalendarSearchResultDTO
    {
        public int AnnualLeaveTrackingDetailID { get; set; }
        public int AnnualLeaveTrackingID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeNM { get; set; }
        public int CompanyID { get; set; }
        public string CompanyNM { get; set; }
        public int AnnualLeaveTypeID { get; set; }
        public string AnnualLeaveTypeNM { get; set; }      
        public string Remark { get; set; }
        public string ManagerRemark { get; set; }
    }
}
