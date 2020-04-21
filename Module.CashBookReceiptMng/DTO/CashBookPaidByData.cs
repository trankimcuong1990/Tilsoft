using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CashBookReceiptMng.DTO
{
    public class CashBookPaidByData
    {
        public int CashBookPaidByID { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int? BalanceMonth { get; set; }
        public int? BalanceYear { get; set; }
    }
}
