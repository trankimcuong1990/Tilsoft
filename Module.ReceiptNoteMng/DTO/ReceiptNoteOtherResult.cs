namespace Module.ReceiptNoteMng.DTO
{
    public class ReceiptNoteOtherResult
    {
        public int? ReceiptNoteOtherID { get; set; }
        public int? ReceiptNoteID { get; set; }
        public int? NoteItemID { get; set; }
        public int? EmployeeID { get; set; }
        public decimal? Amount { get; set; }
        public string Remark { get; set; }
        public string EmployeeUD { get; set; }
        public string EmployeeNM { get; set; }
        public int? ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
    }
}
