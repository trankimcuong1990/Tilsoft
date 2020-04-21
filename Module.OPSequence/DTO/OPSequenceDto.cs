using System.Collections.Generic;

namespace Module.OPSequence.DTO
{
    public class OPSequenceDto
    {
        public int OPSequenceID { get; set; }
        public string OPSequenceNM { get; set; }
        public int? CompanyID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public string ProfileNM { get; set; }
        public string EmployeeNM { get; set; }
        public string CompanyNM { get; set; }

        public List<OPSequenceDetailDto> OPSequenceDetails { get; set; }
    }
}
