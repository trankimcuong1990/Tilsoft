namespace Module.WorkOrder.DTO
{
    public class WorkOrderOPSequenceDTO
    {
        public int WorkOrderOPSequenceID { get; set; }

        public int? WorkOrderID { get; set; }
        public int? OPSequenceDetailID { get; set; }
        public int? WorkCenterID { get; set; }

        public string WorkCenterUD { get; set; }
        public string WorkCenterNM { get; set; }

        public bool? IsActived { get; set; }
        public bool? IsDisabled { get; set; }
        public bool? IsBlocked { get; set; }

        public WorkOrderOPSequenceDTO()
        {
            IsDisabled = false;
            IsActived = false;
            IsBlocked = false;
        }
    }
}
