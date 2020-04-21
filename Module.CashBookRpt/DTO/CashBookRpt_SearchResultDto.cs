namespace Module.CashBookRpt.DTO
{
    public class CashBookRpt_SearchResultDto
    {
        public object KeyID { get; set; }
        public string PostingDate { get; set; }
        public string NoteDate { get; set; }
        public int? ReceiptNoteID { get; set; }
        public string ReceiptNoteNo { get; set; }
        public int? PaymentNoteID { get; set; }
        public string PaymentNoteNo { get; set; }
        public string Remark { get; set; }
        public decimal? TotalBegin { get; set; }
        public decimal? TotalReceipt { get; set; }
        public decimal? TotalPayment { get; set; }
        public decimal? TotalEnd { get; set; }
    }
}
