namespace Module.ReceivingNote.DTO
{
    public class WorkOrderItemDTO
    {
        public int? ProductionItemID { get; set; }
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string FinishedDate { get; set; }
        public decimal? NormQnt { get; set; }
        public int? WorkOrderQnt { get; set; }
        public int PrimaryID { get; set; }

        public WorkOrderItemDTO()
        {
            NormQnt = 0;
            WorkOrderQnt = 0;
        }
    }
}
