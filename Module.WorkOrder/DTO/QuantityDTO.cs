namespace Module.WorkOrder.DTO
{
    public class QuantityDTO
    {
        public int WorkCenterID { get; set; }
        public int QntPlanning { get; set; }
        public int QntCompleted { get; set; }
        public int QntDeliveried { get; set; }
        public int QntRemain { get; set; }
    }
}
