namespace Module.OPSequence.DTO
{
    public class OPSequenceDetailDto
    {
        public int OPSequenceDetailID { get; set; }
        public string OPSequenceDetailNM { get; set; }
        public int? SequenceIndexNumber { get; set; }
        public int? OPSequenceID { get; set; }
        public int? WorkCenterID { get; set; }
        public bool? IsExceptionAtConfirmFrameStatus { get; set; }
    }
}
