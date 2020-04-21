using System.Collections.Generic;

namespace Module.ReceiptNoteMng.DTO
{
    public class SearchForm
    {
        public SearchForm()
        {
            Data = new List<ReceiptNoteSearchResult>();
            receiptSupportStatuses = new List<ReceiptSupportStatus>();
        }
        public List<DTO.ReceiptNoteSearchResult> Data { get; set; }
        public List<DTO.ReceiptSupportStatus> receiptSupportStatuses { get; set; }
        public List<DTO.ReceiptNoteSupportType> receiptNoteSupportTypes { get; set; }
    }
}
