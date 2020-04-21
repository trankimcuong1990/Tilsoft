using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CashBookReceiptMng.DTO
{
    public class CashBookData
    {
        public int CashBookID { get; set; }
        public string CashBookUD { get; set; }
        public string BookDate { get; set; }
        public int? CashBookTypeID { get; set; }
        public string CashBookTypeNM { get; set; }
        public int? CashBookSourceOfFlowID { get; set; }
        public string CashBookSourceOfFlowNM { get; set; }
        public int? CashBookLocationID { get; set; }
        public string CashBookLocationNM { get; set; }
        public int? CashBookPaidByID { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int? CashBookPostCostID { get; set; }
        public string PostCostNM { get; set; }
        public int? CashBookCostItemID { get; set; }
        public string CostItemNM { get; set; }
        public int? CashBookCostItemDetailID { get; set; }
        public string CostItemDetailNM { get; set; }
        public string CashBookReceiverName { get; set; }
        public string CashBookOtherCostItemDetail { get; set; }
        public decimal? VNDAmount { get; set; }
        public decimal? VNDUSDExRate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int ReturnData { get; set; }
        public bool? IsPastTime { get; set; }

        public List<CashBookDocumentData> CashBookDocuments { get; set; }
    }
}
