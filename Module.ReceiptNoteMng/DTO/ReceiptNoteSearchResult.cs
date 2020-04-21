namespace Module.ReceiptNoteMng.DTO
{
    public class ReceiptNoteSearchResult
    {
        public int? ReceiptNoteID { get; set; }
        public int? ReceiptNoteTypeID { get; set; }
        public string ReceiptNoteNo { get; set; }
        public string ReceiptNoteDate { get; set; }
        public string PostingDate { get; set; }
        public string Currency { get; set; }
        public int? ClientID { get; set; }
        public int? ReceiveTypeID { get; set; }
        public string BankAcc { get; set; }
        public string ReceiverName { get; set; }
        public string StatusNM { get; set; }
        public string AttachedFile { get; set; }
        public string Remark { get; set; }
        public string FileLocation { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string Creator { get; set; }
        public string Updater { get; set; }
        public int? StatusID { get; set; }
        public string UpdateDate { get; set; }
        public string CreateDate { get; set; }
        public string ReceiptNoteTypeNM { get; set; }
    }
}
