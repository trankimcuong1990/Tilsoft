using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AnnualLeaveRpt.DTO
{
    public class DetailTotalDTO
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public decimal? NumberOfDays { get; set; }
        public string Remark { get; set; }
        public string ManagerRemark { get; set; }
    }
}
