using System.Collections.Generic;

namespace Module.ReceiptNoteMng.DTO
{
    public class SearchFactorySaleInvoice
    {
        public SearchFactorySaleInvoice()
        {
            Data = new List<ReceiptNoteSupportSearchFactorySaleInvoice>();
        }
        public int totalRows { get; set; }
        public List<DTO.ReceiptNoteSupportSearchFactorySaleInvoice> Data { get; set; }
    }
}
