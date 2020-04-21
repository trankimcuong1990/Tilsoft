using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.WAYNMng.DTO
{
    public class LeaveRequest
    {
        public int? LeaveRequestID { get; set; }
        public int? RequesterID { get; set; }
        public string RequesterUD { get; set; }
        public string RequesterNM { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int? FromTime { get; set; }
        public int? ToTime { get; set; }
        public decimal? TotalDays { get; set; }
        public bool? IsApproved { get; set; }
        public int? DirectorApprovedBy { get; set; }
        public int? ManagerApprovedBy { get; set; }
        public int? LeaveTypeID { get; set; }
        public object ReasonForLeave { get; set; }
        public bool? IsDenied { get; set; }
        public object DenialComment { get; set; }
    }
}
