using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductWizardMng.DTO
{
    public class BreakdownManagementFeeDTO
    {
        public long KeyID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<decimal> QuantityPercent { get; set; }
    }
}
