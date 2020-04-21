namespace Module.WorkOrder.DTO
{
    public class PreOrderWorkOrderBaseOnManagement
    {
        public int PreOrderWorkOrderID { get; set; }
        public int ProductionItemID { get; set; }

        public string ProductionItemUD { get; set; }

        public decimal? PlanQuantity { get; set; }
        public decimal? TransferQuantity { get; set; }
        public decimal? UseQuantity { get; set; }
    }
}
