using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CashBookReceiptMng.DTO
{
    public class EditFormData
    {
        public CashBookData Data { get; set; }

        public List<CashBookTypeData> CashBookTypes { get; set; }
        public List<CashBookSourceOfFlowData> CashBookSourceOfFlows { get; set; }
        public List<CashBookLocationData> CashBookLocations { get; set; }
        public List<CashBookPostCostData> CashBookPostCosts { get; set; }
        public List<CashBookCostItemData> CashBookCostItems { get; set; }
        public List<CashBookCostItemDetailData> CashBookCostItemDetails { get; set; }
        public List<CashBookPaidByData> CashBookPaidBys { get; set; }
    }
}
