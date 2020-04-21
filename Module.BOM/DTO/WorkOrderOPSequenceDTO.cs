namespace Module.BOM.DTO
{
    public class WorkOrderOPSequenceDTO
    {
        public long WorkOrderOPSequenceID { get; set; }

        public int? WorkOrderID { get; set; }
        public int? OPSequenceDetailID { get; set; }
        public int? WorkCenterID { get; set; }
        public int? SequenceIndexNumber { get; set; }

        public string WorkCenterUD { get; set; }
        public string WorkCenterNM { get; set; }
    }
}
