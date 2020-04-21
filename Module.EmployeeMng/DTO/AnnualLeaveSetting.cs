using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.EmployeeMng.DTO
{
    public class AnnualLeaveSetting
    {
        public int? AnnualLeaveSettingID { get; set; }
        public int? EmployeeID { get; set; }
        public int? AffectedYear { get; set; }
        public int? NumberOfDay { get; set; }
        public bool? IsDefault { get; set; }
        public string Comment { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}