namespace Module.WorkOrder.DTO
{
    public class PreOrderWorkOrderManagement
    {
        public int PreOrderWorkOrderID { get; set; }
        public int ProductionItemID { get; set; }
        public int OPSequenceDetailID { get; set; }

        public string ProductionItemUD { get; set; }

        public decimal? PlanQuantity { get; set; }
        public decimal? ProduceQuantity { get; set; }
        public decimal? TransferQuantity { get; set; }
    }
}
