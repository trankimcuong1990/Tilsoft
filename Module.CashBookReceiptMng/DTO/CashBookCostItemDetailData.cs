using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CashBookReceiptMng.DTO
{
    public class CashBookCostItemDetailData
    {
        public int CashBookCostItemDetailID { get; set; }
        public string CostItemDetailNM { get; set; }
        public int? CashBookCostItemID { get; set; }
        public string CostItemNM { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int? DisplayIndex { get; set; }
    }
}
