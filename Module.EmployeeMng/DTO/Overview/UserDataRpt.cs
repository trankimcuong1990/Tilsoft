using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EmployeeMng.DTO.Overview
{
    public class UserDataRpt
    {
        public int? WeekIndex { get; set; }
        public string WeekStart { get; set; }
        public string WeekEnd { get; set; }
        public int cHigh { get; set; }
        public int cIntense { get; set; }
        public int cAboveAverage { get; set; }
        public int cAverage { get; set; }
        public int cLow { get; set; }
        public int cVeryLow { get; set; }
        public int cNone { get; set; }
    }
}
