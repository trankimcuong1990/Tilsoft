using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CashBookReceiptMng.DTO
{
    public class CashBookPostCostData
    {
        public int CashBookPostCostID { get; set; }
        public string PostCostNM { get; set; }
        public int? DisplayIndex { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }

        public List<CashBookCostItemData> CashBookCostItemDatas { get; set; }
    }
}
