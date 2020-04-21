namespace Module.OutsourceRpt.DTO
{
    public class OutsourceWorkOrderDTO
    {
        public int WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public decimal? TotalDelivering { get; set; }
        public decimal? TotalReceiving { get; set; }
        public decimal? TotalEnding { get; set; }
        public bool IsShowWorkOrder { get; set; }

        public OutsourceWorkOrderDTO()
        {
            IsShowWorkOrder = true;
        }
    }
}
