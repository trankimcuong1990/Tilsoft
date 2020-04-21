using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CashBookBalanceMng.DTO
{
    public class CashBookBalance
    {
        public int? CashBookBalanceID { get; set; }
        public int? BalanceMonth { get; set; }
        public int? BalanceYear { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
    }
}
