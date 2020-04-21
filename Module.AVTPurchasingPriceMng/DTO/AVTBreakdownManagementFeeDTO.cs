using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVTPurchasingPriceMng.DTO
{
    public class AVTBreakdownManagementFeeDTO
    {
        public object KeyID { get; set; }
        public int? ModelID { get; set; }
        public int? BreakdownID { get; set; }
        public int? CompanyID { get; set; }
        public decimal? QuantityPercent { get; set; }
    }
}
