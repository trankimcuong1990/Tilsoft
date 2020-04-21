namespace Module.ReceiptNoteMng.DTO
{
    public class ReceiptNoteInvoiceResult
    {
        public int? ReceiptNoteInvoiceID { get; set; }
        public int? ReceiptNoteID { get; set; }
        public int? PurchasingInvoiceID { get; set; }
        public decimal? Amount { get; set; }
        public string Remark { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public decimal? TotalReceipt { get; set; }
        public decimal? Remain { get; set; }
        public decimal? AmountByCurrency { get; set; }
        public string Currency { get; set; }
    }
}
