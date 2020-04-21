namespace Module.WorkOrder.DTO
{
    public class OPSequenceDetailDTO
    {
        public int OPSequenceDetailID { get; set; }

        public int? WorkCenterID { get; set; }
        public int? OPSequenceID { get; set; }

        public string WorkCenterUD { get; set; }
        public string WorkCenterNM { get; set; }

        public bool? IsDisabled { get; set; }
        public bool? IsActived { get; set; }

        public OPSequenceDetailDTO()
        {
            IsDisabled = false;
            IsActived = false;
        }
    }
}
