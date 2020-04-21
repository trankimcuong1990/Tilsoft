using System.Collections.Generic;

namespace Module.SampleItemMng.DTO
{
    public class SampleQARemarkDTO
    {
        public int SampleQARemarkID { get; set; }
        public int? SampleProductID { get; set; }
        public string Remark { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public List<DTO.SampleQARemarkImageDTO> SampleQARemarkImageDTOs { get; set; }
    }
}
