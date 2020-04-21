using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarPurchasingPriceMng.DTO
{
    public class EurofarBreakdownManagementFeeDTO
    {
        public long KeyID { get; set; }
        public int? ModelID { get; set; }
        public int? BreakdownID { get; set; }
        public decimal? QuantityPercent { get; set; }
        public int? CompanyID { get; set; }
    }
}
