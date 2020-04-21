namespace Module.BOM.DTO
{
    public class SupportWorkOrderOPSequenceDTO
    {
        public int PrimaryID { get; set; }

        public int? WorkOrderID { get; set; }
        public int? OPSequenceDetailID { get; set; }
        public int? SequenceIndexNumber { get; set; }
        public int? OPSequenceID { get; set; }
        public int? WorkCenterID { get; set; }
        public int? NextWorkCenterID { get; set; }
        public int? DefaultFactoryWarehouseID { get; set; }

        public string OPSequenceDetailNM { get; set; }
        public string OPSequenceNM { get; set; }
        public string WorkCenterNM { get; set; }
        public string FactoryWarehouseNM { get; set; }

        public decimal? OperatingTime { get; set; }
        public decimal? DefaultCost { get; set; }

        public bool? IsExceptionAtConfirmFrameStatus { get; set; }
    }
}
