using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EmployeeMng.DTO
{
    public class TilsoftUsage
    {
        public int? UserID { get; set; }
        public string AppUsage { get; set; }
        public int? AppUsageID { get; set; }
        public int? TotalHitCount { get; set; }
        public int? AverageHitCount { get; set; }
    }
}
