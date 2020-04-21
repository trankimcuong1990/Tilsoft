using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class BreakdownManagementFee
    {
        public int? ModelID { get; set; }
        public int CompanyID { get; set; }
        public decimal QuantityPercent { get; set; }
    }
}
