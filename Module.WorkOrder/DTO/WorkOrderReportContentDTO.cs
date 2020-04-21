namespace Module.WorkOrder.DTO
{
    public class WorkOrderReportContentDTO
    {
        public int WorkCenterID { get; set; }
        public int WorkOrderID { get; set; }
        public int ProductionItemID { get; set; }
        public int Type { get; set; }
        public int Quantity { get; set; }
    }
}
