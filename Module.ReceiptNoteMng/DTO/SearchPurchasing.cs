using System.Collections.Generic;

namespace Module.ReceiptNoteMng.DTO
{
    public class SearchPurchasing
    {
        public SearchPurchasing()
        {
            Data = new List<ReceiptNoteSupportSerachPurchasingInvoice>();
        }
        public int totalRows { get; set; }
        public List<DTO.ReceiptNoteSupportSerachPurchasingInvoice> Data { get; set; }
    }
}
