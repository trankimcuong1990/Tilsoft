using System.Collections.Generic;

namespace Module.AnnualLeaveMng.DTO
{
    public class EditFormData
    {
        public AnnualLeaveTrackingDTO AnnualLeaveTrackingDTO { get; set; }
        public List<EmployeeDTO> EmployeeDTOs { get; set; }
        public List<AnnualLeaveTypeDTO> AnnualLeaveTypeDTOs { get; set; }
        public List<AnnualLeaveStatusDTO> AnnualLeaveStatusDTOs { get; set; }
        public List<string> FromTimeRange { get; set; }
        public List<string> ToTimeRange { get; set; }
    }
}
