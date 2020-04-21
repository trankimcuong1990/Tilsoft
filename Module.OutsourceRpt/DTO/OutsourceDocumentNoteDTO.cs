namespace Module.OutsourceRpt.DTO
{
    public class OutsourceDocumentNoteDTO
    {
        public int? WorkOrderID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? ImportNoteID { get; set; }
        public int? ExportNoteID { get; set; }
        public string ImportNoteUD { get; set; }
        public string ExportNoteUD { get; set; }
        public string DocumentNoteDate { get; set; }
        public decimal? ReceivingQnt { get; set; }
        public decimal? DeliveringQnt { get; set; }
        public int PrimaryID { get; set; }
        public string ViewName { get; set; }
        public int? DocumentTypeID { get; set; }
    }
}
