using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AnnualLeaveMng.DTO
{
    public class AnnualLeaveTrackingDetailDTO
    {
        public int AnnualLeaveTrackingDetailID { get; set; }
        public Nullable<int> AnnualLeaveTrackingID { get; set; }
        public Nullable<int> LeaveTypeID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public Nullable<decimal> NumberOfDays { get; set; }
    }
}
