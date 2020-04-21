using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CashBookBalanceMng.DTO
{
    public class CashBookBalanceDetail
    {
        public int? CashBookBalanceDetailID { get; set; }
        public int? CashBookBalanceID { get; set; }
        public int? CashBookPaidByID { get; set; }
        public decimal? VNDAmount { get; set; }
        public string PaidByNM { get; set; }
    }
}
