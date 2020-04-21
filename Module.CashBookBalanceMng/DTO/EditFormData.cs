using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CashBookBalanceMng.DTO
{
    public class EditFormData
    {
        public DTO.CashBookBalance CashBookBalance { get; set; }
        public List<DTO.CashBookBalanceDetail> CashBookBalanceDetails { get; set; }
    }
}
