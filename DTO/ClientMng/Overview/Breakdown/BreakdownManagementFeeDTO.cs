using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.Breakdown
{
    public class BreakdownManagementFeeDTO
    {
        public long KeyID { get; set; }
        public int? ModelID { get; set; }
        public int? BreakdownID { get; set; }
        public decimal? QuantityPercent { get; set; }
        public int? CompanyID { get; set; }
    }
}
