using System;
using System.Collections.Generic;

namespace Module.AnnualLeaveMng.DTO
{
    public class AnnualLeaveTrackingDTO
    {
        public List<AnnualLeaveTrackingDetailDTO> AnnualLeaveTrackingDetailDTOs { get; set; }
        public AnnualLeaveTrackingDTO()
        {
            AnnualLeaveTrackingDetailDTOs = new List<AnnualLeaveTrackingDetailDTO>();
        }
        public int AnnualLeaveTrackingID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<int> AffectedYear { get; set; }
        public string Remark { get; set; }
        public string ManagerRemark { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> StatusUpdatedBy { get; set; }
        public string StatusUpdatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public decimal? RequestedTimeOff { get; set; }      
        public decimal? TotalAvailableDay { get; set; }
        public decimal? TotalDaysOff { get; set; }

        public int EmployeeLoginID { get; set; }
        public int StatusUpdatedEmployeeIDBy { get; set; }
        public Nullable<int> ReportToID { get; set; }
        public Nullable<int> ApproveBy { get; set; }
    }
}
